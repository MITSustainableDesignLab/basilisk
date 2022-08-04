using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class ZoneConditioning : LibraryComponent
    {
        [DataMember]
        public YearSchedule CoolingSchedule { get; set; }

        [DataMember]
        public YearSchedule CoolingSetpointSchedule { get; set; }

        [DataMember]
        public YearSchedule HeatingSetpointSchedule { get; set; }

        [DataMember]
        public double CoolingCoeffOfPerf { get; set; }

        [DataMember, DefaultValue(26)]
        public double CoolingSetpoint { get; set; } = 26;

        [DataMember, DefaultValue(IdealSystemLimit.NoLimit)]
        public IdealSystemLimit CoolingLimitType { get; set; } = IdealSystemLimit.NoLimit;

        [DataMember, DefaultValue(FuelType.Electricity)]
        public FuelType CoolingFuelType { get; set; } = FuelType.Electricity;

        [DataMember, DefaultValue(EconomizerType.NoEconomizer)]
        public EconomizerType EconomizerType { get; set; } = EconomizerType.NoEconomizer;

        [DataMember]
        public double HeatingCoeffOfPerf { get; set; }

        [DataMember, DefaultValue(IdealSystemLimit.NoLimit)]
        public IdealSystemLimit HeatingLimitType { get; set; } = IdealSystemLimit.NoLimit;

        [DataMember, DefaultValue(FuelType.NaturalGas)]
        public FuelType HeatingFuelType { get; set; } = FuelType.NaturalGas;

        [DataMember]
        public YearSchedule HeatingSchedule { get; set; }

        [DataMember, DefaultValue(20)]
        public double HeatingSetpoint { get; set; } = 20;

        [DataMember, DefaultValue(0.65)]
        public double HeatRecoveryEfficiencyLatent { get; set; } = 0.65;

        [DataMember, DefaultValue(0.7)]
        public double HeatRecoveryEfficiencySensible { get; set; } = 0.7;

        [DataMember, DefaultValue(HeatRecoveryType.None)]
        public HeatRecoveryType HeatRecoveryType { get; set; } = HeatRecoveryType.None;

        [DataMember, DefaultValue(true)]
        public bool IsCoolingOn { get; set; } = true;

        [DataMember, DefaultValue(true)]
        public bool IsHeatingOn { get; set; } = true;

        [DataMember, DefaultValue(true)]
        public bool IsMechVentOn { get; set; } = true;

        [DataMember, DefaultValue(true)]
        public bool IsCoolingSetpointConstant { get; set; } = true;

        [DataMember, DefaultValue(true)]
        public bool IsHeatingSetpointConstant { get; set; } = true;

        [DataMember, DefaultValue(100)]
        public double MaxCoolFlow { get; set; } = 100;

        [DataMember, DefaultValue(100)]
        public double MaxCoolingCapacity { get; set; } = 100;

        [DataMember, DefaultValue(100)]
        public double MaxHeatFlow { get; set; } = 100;

        [DataMember, DefaultValue(100)]
        public double MaxHeatingCapacity { get; set; } = 100;

        [DataMember]
        public YearSchedule MechVentSchedule { get; set; }

        [DataMember, DefaultValue(0.001)]
        public double MinFreshAirPerArea { get; set; } = 0.001;

        [DataMember, DefaultValue(0.001)]
        public double MinFreshAirPerPerson { get; set; } = 0.001;

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            new LibraryComponent[]
            {
                CoolingSchedule,
                HeatingSchedule,
                MechVentSchedule
            }.Where(s => s != null);
    }
}
