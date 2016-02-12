using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneVentilation))]
    [DisplayName("zone ventilation")]
    [ComponentNamespace]
    public class ZoneVentilation : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Infiltration")]
        [DefaultValue(true)]
        public bool IsInfiltrationOn { get; set; } = true;

        [SimulationSetting(DisplayName = "Infiltration rate", Units = "ACH")]
        [DefaultValue(0.1)]
        public double Infiltration { get; set; } = 0.1;

        [SimulationSetting(DisplayName = "Natural ventilation")]
        public bool IsNatVentOn { get; set; }

        [SimulationSetting(DisplayName = "Nat vent min outdoor air temp", Units = "degC")]
        public double NatVentMinOutdoorAirTemp { get; set; }

        [SimulationSetting(DisplayName = "Nat vent max outdoor air temp", Units = "degC")]
        [DefaultValue(30)]
        public double NatVentMaxOutdoorAirTemp { get; set; } = 30;

        [SimulationSetting(DisplayName = "Nat vent max rel humidity")]
        [DefaultValue(90)]
        public double NatVentMaxRelHumidity { get; set; } = 90;

        [SimulationSetting(DisplayName = "Nat vent schedule")]
        public YearSchedule NatVentSchedule { get; set; }

        [SimulationSetting(DisplayName = "Nat vent zone temperature setpoint", Units = "degC")]
        [DefaultValue(18)]
        public double NatVentZoneTempSetpoint { get; set; } = 18;

        [SimulationSetting(DisplayName = "Scheduled ventilation")]
        public bool IsScheduledVentilationOn { get; set; }

        [SimulationSetting(DisplayName = "Scheduled ventilation ACH", Units = "ACH")]
        [DefaultValue(0.6)]
        public double ScheduledVentilationAch { get; set; } = 0.6;

        [SimulationSetting(DisplayName = "Scheduled ventilation schedule")]
        public YearSchedule ScheduledVentilationSchedule { get; set; }

        [SimulationSetting(DisplayName = "Scheduled ventilation setpoint", Units = "degC")]
        [DefaultValue(18)]
        public double ScheduledVentilationSetpoint { get; set; } = 18;

        [SimulationSetting(DisplayName = "Buoyancy")]
        [DefaultValue(true)]
        public bool IsBuoyancyOn { get; set; } = true;

        [SimulationSetting(DisplayName = "Wind")]
        public bool IsWindOn { get; set; }

        [SimulationSetting]
        public bool Afn { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    NatVentSchedule,
                    ScheduledVentilationSchedule
                }.Where(d => d != null);
                return
                    direct
                    .Concat(direct.SelectMany(d => d.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            NatVentSchedule == component ||
            ScheduledVentilationSchedule == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneVentilation()
            {
                NatVentSchedule = NatVentSchedule,
                ScheduledVentilationSchedule = ScheduledVentilationSchedule,
            };
            CopyNonReferenceProperties(res, this);
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (ZoneVentilation)other;
            NatVentSchedule = coord.GetWithSameName(c.NatVentSchedule);
            ScheduledVentilationSchedule = coord.GetWithSameName(c.ScheduledVentilationSchedule);
            CopyNonReferenceProperties(this, c);
            CopyBasePropertiesFrom(c);
        }

        private static void CopyNonReferenceProperties(ZoneVentilation to, ZoneVentilation from)
        {
            to.IsInfiltrationOn = from.IsInfiltrationOn;
            to.Infiltration = from.Infiltration;
            to.IsNatVentOn = from.IsNatVentOn;
            to.NatVentMinOutdoorAirTemp = from.NatVentMinOutdoorAirTemp;
            to.NatVentMaxOutdoorAirTemp = from.NatVentMaxOutdoorAirTemp;
            to.NatVentMaxRelHumidity = from.NatVentMaxRelHumidity;
            to.NatVentZoneTempSetpoint = from.NatVentZoneTempSetpoint;
            to.IsScheduledVentilationOn = from.IsScheduledVentilationOn;
            to.ScheduledVentilationAch = from.ScheduledVentilationAch;
            to.ScheduledVentilationSetpoint = from.ScheduledVentilationSetpoint;
            to.IsBuoyancyOn = from.IsBuoyancyOn;
            to.IsWindOn = from.IsWindOn;
            to.Afn = from.Afn;
        }
    }
}
