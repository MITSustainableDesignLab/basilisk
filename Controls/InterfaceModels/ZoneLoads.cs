using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Core;
using Basilisk.Controls.Attributes;
using System;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneLoads))]
    [DisplayName("zone loads")]
    [ComponentNamespace]
    public class ZoneLoads : LibraryComponent
    {
        [SimulationSetting(DisplayName = "People")]
        public bool IsPeopleOn { get; set; }

        [SimulationSetting(DisplayName = "Occupancy density (p/m2)")]
        public double PeopleDensity { get; set; }

        [SimulationSetting(DisplayName = "Occupancy schedule")]
        public YearSchedule OccupancySchedule { get; set; }

        [SimulationSetting(DisplayName = "Equipment")]
        public bool IsEquipmentOn { get; set; }

        [SimulationSetting(DisplayName = "Equipment power density (W/m2)")]
        public double EquipmentPowerDensity { get; set; }

        [SimulationSetting(DisplayName = "Equipment availability schedule")]
        public YearSchedule EquipmentAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Lighting")]
        public bool IsLightingOn { get; set; }

        [SimulationSetting(DisplayName = "Lighting power density (W/m2)")]
        public double LightingPowerDensity { get; set; }

        [SimulationSetting(DisplayName = "Lighting availability schedule")]
        public YearSchedule LightsAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Dimming type")]
        public DimmingType DimmingType { get; set; }

        [SimulationSetting(DisplayName = "Illuminance target (lux)")]
        public double IlluminanceTarget { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    OccupancySchedule,
                    EquipmentAvailabilitySchedule,
                    LightsAvailabilitySchedule
                }.Where(d => d != null);
                return
                    direct
                    .Concat(direct.SelectMany(d => d.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            OccupancySchedule == component ||
            EquipmentAvailabilitySchedule == component ||
            LightsAvailabilitySchedule == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneLoads()
            {
                EquipmentAvailabilitySchedule = EquipmentAvailabilitySchedule,
                LightsAvailabilitySchedule = LightsAvailabilitySchedule,
                OccupancySchedule = OccupancySchedule,
            };
            CopyNonReferenceProperties(res, this);
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (ZoneLoads)other;
            CopyNonReferenceProperties(this, c);
            EquipmentAvailabilitySchedule = coord.GetWithSameName(c.EquipmentAvailabilitySchedule);
            LightsAvailabilitySchedule = coord.GetWithSameName(c.LightsAvailabilitySchedule);
            OccupancySchedule = coord.GetWithSameName(c.OccupancySchedule);
            CopyBasePropertiesFrom(c);
        }

        private static void CopyNonReferenceProperties(ZoneLoads to, ZoneLoads from)
        {
            to.DimmingType = from.DimmingType;
            to.EquipmentPowerDensity = from.EquipmentPowerDensity;
            to.IlluminanceTarget = from.IlluminanceTarget;
            to.LightingPowerDensity = from.LightingPowerDensity;
            to.IsEquipmentOn = from.IsEquipmentOn;
            to.IsLightingOn = from.IsLightingOn;
            to.IsPeopleOn = from.IsPeopleOn;
            to.PeopleDensity = from.PeopleDensity;
        }
    }
}
