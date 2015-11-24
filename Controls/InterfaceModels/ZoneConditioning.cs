using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneConditioning))]
    [DisplayName("zone conditioning")]
    public class ZoneConditioning : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Heating")]
        public bool IsHeatingOn { get; set; }

        [SimulationSetting(DisplayName = "Heating setpoint (C)")]
        public double HeatingSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Heating schedule")]
        public YearSchedule HeatingSchedule { get; set; }

        [SimulationSetting(DisplayName = "Heating limit type")]
        public string HeatingLimitType { get; set; }

        [SimulationSetting(DisplayName = "Max heating capacity (W/m2)")]
        public double MaxHeatingCapacity { get; set; }

        [SimulationSetting(DisplayName = "Max heat flow (m3/s)")]
        public double MaxHeatFlow { get; set; }

        [SimulationSetting(DisplayName = "Cooling")]
        public bool IsCoolingOn { get; set; }

        [SimulationSetting(DisplayName = "Cooling setpoint (C)")]
        public double CoolingSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Cooling schedule")]
        public YearSchedule CoolingSchedule { get; set; }

        [SimulationSetting(DisplayName = "Cooling limit type")]
        public string CoolingLimitType { get; set; }

        [SimulationSetting(DisplayName = "Max cooling capacity (W/m2)")]
        public double MaxCoolingCapacity { get; set; }

        [SimulationSetting(DisplayName = "Max cool flow (m3/s/m2)")]
        public double MaxCoolFlow { get; set; }

        [SimulationSetting(DisplayName = "Mechanical ventilation")]
        public bool IsMechVentOn { get; set; }

        [SimulationSetting(DisplayName = "Mechanical ventilation schedule")]
        public YearSchedule MechVentSchedule { get; set; }

        [SimulationSetting(DisplayName = "Min fresh air per area (m3/s/m2)")]
        public double MinFreshAirPerArea { get; set; }

        [SimulationSetting(DisplayName = "Min fresh air per person (m3/s/p)")]
        public double MinFreshAirPerPerson { get; set; }

        [SimulationSetting(DisplayName = "Economizer type")]
        public string EconomizerType { get; set; }

        [SimulationSetting(DisplayName = "Heat recovery type")]
        public string HeatRecoveryType { get; set; }

        [SimulationSetting(DisplayName = "Heat recovery efficiency (latent)")]
        public double HeatRecoveryEfficiencyLatent { get; set; }

        [SimulationSetting(DisplayName = "Heat recovery efficiency (sensible)")]
        public double HeatRecoveryEfficiencySensible { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            HeatingSchedule == component ||
            CoolingSchedule == component ||
            MechVentSchedule == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneConditioning()
            {
                IsHeatingOn = IsHeatingOn,
                HeatingSetpoint = HeatingSetpoint,
                HeatingSchedule = HeatingSchedule,
                HeatingLimitType = HeatingLimitType,
                MaxHeatingCapacity = MaxHeatingCapacity,
                MaxHeatFlow = MaxHeatFlow,
                IsCoolingOn = IsCoolingOn,
                CoolingSetpoint = CoolingSetpoint,
                CoolingSchedule = CoolingSchedule,
                CoolingLimitType = CoolingLimitType,
                MaxCoolingCapacity = MaxCoolingCapacity,
                MaxCoolFlow = MaxCoolFlow,
                IsMechVentOn = IsMechVentOn,
                MechVentSchedule = MechVentSchedule,
                MinFreshAirPerArea = MinFreshAirPerArea,
                MinFreshAirPerPerson = MinFreshAirPerPerson,
                EconomizerType = EconomizerType,
                HeatRecoveryType = HeatRecoveryType,
                HeatRecoveryEfficiencyLatent = HeatRecoveryEfficiencyLatent,
                HeatRecoveryEfficiencySensible = HeatRecoveryEfficiencySensible
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
