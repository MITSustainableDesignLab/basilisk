using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Core;
using Basilisk.Controls.Attributes;
using System;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneConditioning))]
    [DisplayName("zone conditioning")]
    [ComponentNamespace]
    public class ZoneConditioning : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Heating")]
        [DefaultValue(true)]
        public bool IsHeatingOn { get; set; } = true;

        [SimulationSetting(DisplayName = "Heating setpoint", Units = "degC")]
        [DefaultValue(20)]
        public double HeatingSetpoint { get; set; } = 20;

        [SimulationSetting(DisplayName = "Heating schedule")]
        public YearSchedule HeatingSchedule { get; set; }

        [SimulationSetting(DisplayName = "Heating limit type")]
        [DefaultValue(IdealSystemLimit.NoLimit)]
        public IdealSystemLimit HeatingLimitType { get; set; } = IdealSystemLimit.NoLimit;

        [SimulationSetting(DisplayName = "Max heating capacity", Units = "W/m2")]
        [DefaultValue(100)]
        public double MaxHeatingCapacity { get; set; } = 100;

        [SimulationSetting(DisplayName = "Max heat flow", Units = "m3/s/m2")]
        [DefaultValue(100)]
        public double MaxHeatFlow { get; set; } = 100;

        [SimulationSetting(DisplayName = "Heating CoP")]
        [DefaultValue(1.0)]
        public double HeatingCoeffOfPerf { get; set; } = 1.0;

        [SimulationSetting(DisplayName = "Cooling")]
        [DefaultValue(true)]
        public bool IsCoolingOn { get; set; } = true;

        [SimulationSetting(DisplayName = "Cooling setpoint", Units = "degC")]
        [DefaultValue(26)]
        public double CoolingSetpoint { get; set; } = 26;

        [SimulationSetting(DisplayName = "Cooling schedule")]
        public YearSchedule CoolingSchedule { get; set; }

        [SimulationSetting(DisplayName = "Cooling limit type")]
        [DefaultValue(IdealSystemLimit.NoLimit)]
        public IdealSystemLimit CoolingLimitType { get; set; } = IdealSystemLimit.NoLimit;

        [SimulationSetting(DisplayName = "Max cooling capacity", Units = "W/m2")]
        [DefaultValue(100)]
        public double MaxCoolingCapacity { get; set; } = 100;

        [SimulationSetting(DisplayName = "Max cool flow", Units = "m3/s/m2")]
        [DefaultValue(100)]
        public double MaxCoolFlow { get; set; } = 100;

        [SimulationSetting(DisplayName = "Cooling CoP")]
        [DefaultValue(1.0)]
        public double CoolingCoeffOfPerf { get; set; } = 1.0;

        [SimulationSetting(DisplayName = "Mechanical ventilation")]
        [DefaultValue(true)]
        public bool IsMechVentOn { get; set; } = true;

        [SimulationSetting(DisplayName = "Mechanical ventilation schedule")]
        public YearSchedule MechVentSchedule { get; set; }

        [SimulationSetting(DisplayName = "Min fresh air per area", Units = "m3/s/m2")]
        [DefaultValue(0.001)]
        public double MinFreshAirPerArea { get; set; } = 0.001;

        [SimulationSetting(DisplayName = "Min fresh air per person", Units = "m3/s/p")]
        [DefaultValue(0.001)]
        public double MinFreshAirPerPerson { get; set; } = 0.001;

        [SimulationSetting(DisplayName = "Economizer type")]
        [DefaultValue(EconomizerType.NoEconomizer)]
        public EconomizerType EconomizerType { get; set; } = EconomizerType.NoEconomizer;

        [SimulationSetting(DisplayName = "Heat recovery type")]
        [DefaultValue(HeatRecoveryType.None)]
        public HeatRecoveryType HeatRecoveryType { get; set; } = HeatRecoveryType.None;

        [SimulationSetting(DisplayName = "Heat recovery efficiency (latent)")]
        public double HeatRecoveryEfficiencyLatent { get; set; } = 0.65;

        [SimulationSetting(DisplayName = "Heat recovery efficiency (sensible)")]
        public double HeatRecoveryEfficiencySensible { get; set; } = 0.7;

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    HeatingSchedule,
                    CoolingSchedule,
                    MechVentSchedule
                }.Where(d => d != null);
                return
                    direct
                    .Concat(direct.SelectMany(d => d.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            HeatingSchedule == component ||
            CoolingSchedule == component ||
            MechVentSchedule == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneConditioning()
            {
                HeatingSchedule = HeatingSchedule,
                CoolingSchedule = CoolingSchedule,
                MechVentSchedule = MechVentSchedule,
            };
            CopyNonReferenceProperties(res, this);
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (ZoneConditioning)other;
            CopyNonReferenceProperties(this, c);
            HeatingSchedule = coord.GetWithSameName(c.HeatingSchedule);
            CoolingSchedule = coord.GetWithSameName(c.CoolingSchedule);
            MechVentSchedule = coord.GetWithSameName(c.MechVentSchedule);
            CopyBasePropertiesFrom(c);
        }

        private static void CopyNonReferenceProperties(ZoneConditioning to, ZoneConditioning from)
        {
            to.IsHeatingOn = from.IsHeatingOn;
            to.HeatingSetpoint = from.HeatingSetpoint;
            to.HeatingLimitType = from.HeatingLimitType;
            to.MaxHeatingCapacity = from.MaxHeatingCapacity;
            to.MaxHeatFlow = from.MaxHeatFlow;
            to.IsCoolingOn = from.IsCoolingOn;
            to.CoolingCoeffOfPerf = from.CoolingCoeffOfPerf;
            to.CoolingSetpoint = from.CoolingSetpoint;
            to.CoolingLimitType = from.CoolingLimitType;
            to.MaxCoolingCapacity = from.MaxCoolingCapacity;
            to.MaxCoolFlow = from.MaxCoolFlow;
            to.IsMechVentOn = from.IsMechVentOn;
            to.MinFreshAirPerArea = from.MinFreshAirPerArea;
            to.MinFreshAirPerPerson = from.MinFreshAirPerPerson;
            to.EconomizerType = from.EconomizerType;
            to.HeatRecoveryType = from.HeatRecoveryType;
            to.HeatRecoveryEfficiencyLatent = from.HeatRecoveryEfficiencyLatent;
            to.HeatRecoveryEfficiencySensible = from.HeatRecoveryEfficiencySensible;
            to.HeatingCoeffOfPerf = from.HeatingCoeffOfPerf;
        }
    }
}
