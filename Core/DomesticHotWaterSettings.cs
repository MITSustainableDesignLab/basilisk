using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class DomesticHotWaterSettings : LibraryComponent
    {
        [DataMember, DefaultValue(0.85)]
        public double CoefficientOfPerformance { get; set; } = 0.85;

        [DataMember]
        public double FlowRatePerFloorArea { get; set; } = 0.03;

        [DataMember, DefaultValue(DomesticHotWaterFuelType.Electricity)]
        public DomesticHotWaterFuelType FuelType { get; set; } = DomesticHotWaterFuelType.NaturalGas;

        [DataMember, DefaultValue(true)]
        public bool IsOn { get; set; } = true;

        [DataMember]
        public YearSchedule WaterSchedule { get; set; }

        [DataMember, DefaultValue(65)]
        public double WaterSupplyTemperature { get; set; } = 65;

        [DataMember, DefaultValue(10)]
        public double WaterTemperatureInlet { get; set; } = 10;

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            Enumerable.Repeat(WaterSchedule, 1).Where(s => s != null);
    }
}
