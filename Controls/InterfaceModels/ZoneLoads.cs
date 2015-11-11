using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    public class ZoneLoads : LibraryComponent
    {
        [SimulationSetting(DisplayName = "People")]
        public bool IsPeopleOn { get; set; } = true;

        [SimulationSetting(DisplayName = "Occupancy density (p/m2)")]
        public double PeopleDensity { get; set; } = 0.2;

        [SimulationSetting(DisplayName = "Occupancy schedule")]
        public YearSchedule OccupancySchedule { get; set; }

        [SimulationSetting(DisplayName = "Equipment")]
        public bool IsEquipmentOn { get; set; }

        [SimulationSetting(DisplayName = "Equipment power density (W/m2)")]
        public double EquipmentPowerDensity { get; set; } = 12;

        [SimulationSetting(DisplayName = "Equipment availability schedule")]
        public YearSchedule EquipmentAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Lighting")]
        public bool IsLightingOn { get; set; }

        [SimulationSetting(DisplayName = "Lighting power density (W/m2)")]
        public double LightingPowerDensity { get; set; } = 12;

        [SimulationSetting(DisplayName = "Lighting availability schedule")]
        public YearSchedule LightsAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Dimming type")]
        public string DimmingType { get; set; } = "Continuous";

        [SimulationSetting(DisplayName = "Illuminance target (lux)")]
        public double IlluminanceTarget { get; set; } = 500;

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
