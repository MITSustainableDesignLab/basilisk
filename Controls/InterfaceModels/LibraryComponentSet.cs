using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    public class LibraryComponentSet : LibraryComponent
    {
        private readonly Lazy<IReadOnlyCollection<SimulationSetting>> settings;

        internal LibraryComponentSet(IEnumerable<LibraryComponent> components)
        {
            if (components == null)
            {
                throw new ArgumentNullException(nameof(components));
            }
            else if (!components.Any())
            {
                throw new ArgumentException("A library component set must contain at least one component", nameof(components));
            }
            Components = components;
            EventHandler<PropertyChangedEventArgs> propChanged = (s, e) => RaisePropertyChanged(s, e.PropertyName);
            foreach (var c in Components)
            {
                WeakEventManager<LibraryComponent, PropertyChangedEventArgs>.AddHandler(c, nameof(PropertyChanged), propChanged);
            }
            settings = new Lazy<IReadOnlyCollection<SimulationSetting>>(CreateSimulationSettings);
        }

        public IEnumerable<LibraryComponent> Components { get; }

        public override string Category
        {
            get { return "(multiple categories)"; }
            set { throw new NotSupportedException(); }
        }

        public override string Comments
        {
            get { return SingleIfExists(Components.Select(c => c.Comments)) ?? "(multiple comments)"; }
            set
            {
                foreach (var c in Components) { c.Comments = value; }
            }
        }

        public override string DataSource
        {
            get { return SingleIfExists(Components.Select(c => c.DataSource)) ?? "(multiple sources)"; }
            set
            {
                foreach (var c in Components) { c.DataSource = value; }
            }
        }

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Components.Concat(Components.SelectMany(c => c.AllReferencedComponents));

        public override bool IsCategoryNameMutable => false;
        public override bool IsNameMutable => false;

        public override string Name
        {
            get { return "(multiple names)"; }
            set { throw new NotSupportedException(); }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            Components.Any(c => c.DirectlyReferences(component));

        public override LibraryComponent Duplicate() =>
            new LibraryComponentSet(Components.Select(c => c.Duplicate()));

        public override IEnumerable<SimulationSetting> SimulationSettings =>
            settings.Value;

        private IReadOnlyCollection<SimulationSetting> CreateSimulationSettings()
        {
            System.Diagnostics.Debug.Assert(Components.Any());
            var sourceTypes = Components.Select(c => c.GetType()).Distinct().ToArray();
            if (sourceTypes.Length > 1)
            {
                throw new InvalidOperationException("Simulation settings cannot be generated for library component sets with components of multiple types");
            }
            var typeOrderer = SimulationSettingsCreator.HierarchyComparer.Build(sourceTypes[0]);
            return
                sourceTypes[0]
                .GetProperties()
                .OrderBy(prop => prop.DeclaringType, typeOrderer)
                .Select(prop => new { Prop = prop, Att = prop.GetCustomAttribute<SimulationSettingAttribute>() })
                .Where(x => x.Att != null)
                .Select(x =>
                {
                    var displayName = x.Att.DisplayName == null ? x.Prop.Name : x.Att.DisplayName;
                    var setting = new SimulationSetting(this, x.Prop, displayName);
                    setting.PropertyChanged += (s, e) => RaisePropertyChanged(setting.PropertyName);
                    return setting;
                })
                .ToArray();
        }

        private static string SingleIfExists(IEnumerable<string> sources)
        {
            System.Diagnostics.Debug.Assert(sources.Any());
            var vals = sources.Distinct();
            return vals.Count() == 1 ? vals.First() : null;
        }
    }
}
