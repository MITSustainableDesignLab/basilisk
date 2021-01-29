using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

using Basilisk.Controls.Attributes;
using System;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.BuildingTemplate))]
    [DisplayName("building template")]
    [ComponentNamespace]
    public class BuildingTemplate : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Core zone type")]
        public ZoneDefinition Core { get; set; }

        [SimulationSetting(DisplayName = "Perimeter zone type")]
        public ZoneDefinition Perimeter { get; set; }

        [SimulationSetting]
        public StructureInformation Structure { get; set; }

        [SimulationSetting(DisplayName = "Partition ratio")]
        public double PartitionRatio { get; set; }

        [SimulationSetting(Units = "years")]
        public int Lifespan { get; set; }

        [SimulationSetting]
        public WindowSettings Windows { get; set; }

        [SimulationSetting(DisplayName = "Default WWR")]
        public double DefaultWindowToWallRatio { get; set; }

        [SimulationSetting(Description = "Start year for range")]
        public int YearFrom { get; set; }

        [SimulationSetting(Description = "End year for range")]
        public int YearTo { get; set; }

        [SimulationSetting(Description = "alpha-3 Country Code; comma separated for multiple values")]
        public string[] Country { get; set; }

        [SimulationSetting(Description = "ANSI/ASHRAE/IESNA Standard 90.1 International Climatic Zone; comma separated for multiple values")]
        public string[] ClimateZone { get; set; }

        [SimulationSetting(Description = "Authors of this template; comma separated for multiple values")]
        public string[] Authors { get; set; }

        [SimulationSetting(Description = "Contact information; comma separated for multiple values")]
        public string[] AuthorEmails { get; set; }

        [SimulationSetting(Description = "Version number")]
        public string Version { get; set; }


        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    Core,
                    Perimeter,
                    Structure,
                    Windows
                };
                return
                    direct
                    .Where(d => d != null)
                    .Concat(direct.SelectMany(d => d.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            component == Core ||
            component == Perimeter ||
            component == Structure ||
            component == Windows;

        public override LibraryComponent Duplicate()
        {
            var res = new BuildingTemplate()
            {
                Core = Core,
                Perimeter = Perimeter,
                Structure = Structure,
                PartitionRatio = PartitionRatio,
                Lifespan = Lifespan,
                Windows = Windows,
                DefaultWindowToWallRatio = DefaultWindowToWallRatio,
                YearFrom = YearFrom,
                YearTo = YearTo,
                Country = Country,
                ClimateZone = ClimateZone,
                Authors = Authors,
                AuthorEmails = AuthorEmails,
                Version = Version
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (BuildingTemplate)other;
            Core = coord.GetWithSameName(c.Core);
            Perimeter = coord.GetWithSameName(c.Perimeter);
            Structure = coord.GetWithSameName(c.Structure);
            Windows = coord.GetWithSameName(c.Windows);
            PartitionRatio = c.PartitionRatio;
            Lifespan = c.Lifespan;
            DefaultWindowToWallRatio = c.DefaultWindowToWallRatio;
            YearFrom = c.YearFrom;
            YearTo = c.YearTo;
            Country = c.Country;
            ClimateZone = c.ClimateZone;
            Authors = c.Authors;
            AuthorEmails = c.AuthorEmails;
            Version = c.Version;
            CopyBasePropertiesFrom(c);
        }
    }
}
