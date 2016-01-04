using Basilisk.Core;
using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneLoads))]
    [DisplayName("zone loads")]
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

        public override bool DirectlyReferences(LibraryComponent component) =>
            OccupancySchedule == component ||
            EquipmentAvailabilitySchedule == component ||
            LightsAvailabilitySchedule == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneLoads()
            {
                DimmingType = DimmingType,
                EquipmentAvailabilitySchedule = EquipmentAvailabilitySchedule,
                EquipmentPowerDensity = EquipmentPowerDensity,
                IlluminanceTarget = IlluminanceTarget,
                LightingPowerDensity = LightingPowerDensity,
                LightsAvailabilitySchedule = LightsAvailabilitySchedule,
                OccupancySchedule = OccupancySchedule,
                IsEquipmentOn = IsEquipmentOn,
                IsLightingOn = IsLightingOn,
                IsPeopleOn = IsPeopleOn,
                PeopleDensity = PeopleDensity
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
