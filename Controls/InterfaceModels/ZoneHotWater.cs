using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.DomesticHotWaterSettings))]
    public class ZoneHotWater : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Enabled")]
        public bool IsOn { get; set; }

        [SimulationSetting(DisplayName = "Schedule")]
        public YearSchedule WaterSchedule { get; set; }

        [SimulationSetting(DisplayName = "Supply temperature")]
        public double WaterSupplyTemperature { get; set; }

        [SimulationSetting(DisplayName = "Inlet temperature")]
        public double WaterTemperatureInlet { get; set; }

        [SimulationSetting(DisplayName = "Flow rate (m3/h/m2)")]
        public double FlowRatePerFloorArea { get; set; }

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneHotWater()
            {
                IsOn = IsOn,
                WaterSchedule = WaterSchedule,
                WaterSupplyTemperature = WaterSupplyTemperature,
                WaterTemperatureInlet = WaterTemperatureInlet,
                FlowRatePerFloorArea = FlowRatePerFloorArea
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
