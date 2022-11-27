using Basilisk.Controls.Attributes;
using Basilisk.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.DomesticHotWaterSettings))]
    [DisplayName("zone hot water")]
    [ComponentNamespace]
    public class ZoneHotWater : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Enabled")]
        public bool IsOn { get; set; }

        [SimulationSetting(DisplayName = "Flow Rate Fraction Schedule")]
        public YearSchedule WaterSchedule { get; set; }

        [SimulationSetting(DisplayName = "Supply temperature", Units = "degC")]
        [DefaultValue(65)]
        public double WaterSupplyTemperature { get; set; } = 65;

        [SimulationSetting(DisplayName = "Inlet temperature", Units = "degC")]
        [DefaultValue(10)]
        public double WaterTemperatureInlet { get; set; } = 10;

        [SimulationSetting(DisplayName = "Peak Flow Rate", Units = "m3/h/m2")]
        [DefaultValue(0.03)]
        public double FlowRatePerFloorArea { get; set; } = 0.03;

        [SimulationSetting(DisplayName = "Fuel type")]
        [DefaultValue(DomesticHotWaterFuelType.Electricity)]
        public DomesticHotWaterFuelType FuelType { get; set; } = DomesticHotWaterFuelType.Electricity;

        [SimulationSetting(DisplayName = "COP")]
        [DefaultValue(0.85)]
        public double CoefficientOfPerformance { get; set; } = 0.85;

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Enumerable
            .Repeat(WaterSchedule, 1)
            .Where(d => d != null)
            .Concat(WaterSchedule.AllReferencedComponents)
            .Distinct();

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

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (ZoneHotWater)other;
            IsOn = c.IsOn;
            WaterSchedule = coord.GetWithSameName(c.WaterSchedule);
            WaterSupplyTemperature = c.WaterSupplyTemperature;
            WaterTemperatureInlet = c.WaterTemperatureInlet;
            FlowRatePerFloorArea = c.FlowRatePerFloorArea;
            CopyBasePropertiesFrom(c);
        }
    }
}
