using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.DomesticHotWaterSettings))]
    [DisplayName("zone hot water")]
    [ComponentNamespace]
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

        public override bool DirectlyReferences(LibraryComponent component) =>
            WaterSchedule == component;

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
