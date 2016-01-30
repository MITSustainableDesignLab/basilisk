using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    public abstract class LibraryComponent : INotifyPropertyChanged
    {
        private string name;

        static LibraryComponent()
        {
            Mapper.CreateMap<Core.LibraryComponent, LibraryComponent>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract IEnumerable<LibraryComponent> AllReferencedComponents { get; }
        public virtual string Category { get; set; }
        public virtual string Comments { get; set; }
        public virtual string DataSource { get; set; }
        public virtual bool IsCategoryNameMutable => true;
        public virtual bool IsNameMutable => true;
        public string TypeDisplayName => GetType().GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? GetType().Name;

        public virtual string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        public abstract bool DirectlyReferences(LibraryComponent component);

        public abstract LibraryComponent Duplicate();

        public override string ToString() => Name;

        public virtual IReadOnlyCollection<SimulationSetting> SimulationSettings(ComponentCoordinator coordinator)
        {
            var sourceType = GetType();
            var typeOrderer = SimulationSettingsCreator.HierarchyComparer.Build(sourceType);
            return
                sourceType
                .GetProperties()
                .OrderBy(prop => prop.DeclaringType, typeOrderer)
                .Select(prop => new { Prop = prop, Att = prop.GetCustomAttribute<SimulationSettingAttribute>() })
                .Where(x => x.Att != null)
                .Select(x =>
                {
                    var displayName = x.Att.DisplayName == null ? x.Prop.Name : x.Att.DisplayName;
                    var setting = new SimulationSetting(this, x.Prop, displayName, coordinator);
                    setting.PropertyChanged += (s, e) => RaisePropertyChanged(setting.PropertyName);
                    return setting;
                })
                .ToArray();
        }

        protected void CopyBasePropertiesFrom(LibraryComponent source)
        {
            Category = source.Category;
            Comments = source.Comments;
            DataSource = source.DataSource;
            Name = source.Name;
        }
    }
}
