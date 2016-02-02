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
        public bool IsHeatingOn { get; set; }

        [SimulationSetting(DisplayName = "Heating setpoint (C)")]
        public double HeatingSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Heating schedule")]
        public YearSchedule HeatingSchedule { get; set; }

        [SimulationSetting(DisplayName = "Heating limit type")]
        public IdealSystemLimit HeatingLimitType { get; set; }

        [SimulationSetting(DisplayName = "Max heating capacity (W/m2)")]
        public double MaxHeatingCapacity { get; set; }

        [SimulationSetting(DisplayName = "Max heat flow (m3/s)")]
        public double MaxHeatFlow { get; set; }

        [SimulationSetting(DisplayName = "Heating CoP")]
        public double HeatingCoeffOfPerf { get; set; }

        [SimulationSetting(DisplayName = "Cooling")]
        public bool IsCoolingOn { get; set; }

        [SimulationSetting(DisplayName = "Cooling setpoint (C)")]
        public double CoolingSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Cooling schedule")]
        public YearSchedule CoolingSchedule { get; set; }

        [SimulationSetting(DisplayName = "Cooling limit type")]
        public IdealSystemLimit CoolingLimitType { get; set; }

        [SimulationSetting(DisplayName = "Max cooling capacity (W/m2)")]
        public double MaxCoolingCapacity { get; set; }

        [SimulationSetting(DisplayName = "Max cool flow (m3/s/m2)")]
        public double MaxCoolFlow { get; set; }

        [SimulationSetting(DisplayName = "Cooling CoP")]
        public double CoolingCoeffOfPerf { get; set; }

        [SimulationSetting(DisplayName = "Mechanical ventilation")]
        public bool IsMechVentOn { get; set; }

        [SimulationSetting(DisplayName = "Mechanical ventilation schedule")]
        public YearSchedule MechVentSchedule { get; set; }

        [SimulationSetting(DisplayName = "Min fresh air per area (m3/s/m2)")]
        public double MinFreshAirPerArea { get; set; }

        [SimulationSetting(DisplayName = "Min fresh air per person (m3/s/p)")]
        public double MinFreshAirPerPerson { get; set; }

        [SimulationSetting(DisplayName = "Economizer type")]
        public EconomizerType EconomizerType { get; set; }

        [SimulationSetting(DisplayName = "Heat recovery type")]
        public HeatRecoveryType HeatRecoveryType { get; set; }

        [SimulationSetting(DisplayName = "Heat recovery efficiency (latent)")]
        public double HeatRecoveryEfficiencyLatent { get; set; }

        [SimulationSetting(DisplayName = "Heat recovery efficiency (sensible)")]
        public double HeatRecoveryEfficiencySensible { get; set; }

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
