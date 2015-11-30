using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneVentilation))]
    [DisplayName("zone ventilation")]
    public class ZoneVentilation : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Infiltration")]
        public bool IsInfiltrationOn { get; set; }

        [SimulationSetting(DisplayName = "Infiltration rate")]
        public double Infiltration { get; set; }

        [SimulationSetting(DisplayName = "Natural ventilation")]
        public bool IsNatVentOn { get; set; }

        [SimulationSetting(DisplayName = "Nat vent min outdoor air temp")]
        public double NatVentMinOutdoorAirTemp { get; set; }

        [SimulationSetting(DisplayName = "Nat vent max outdoor air temp")]
        public double NatVentMaxOutdoorAirTemp { get; set; }

        [SimulationSetting(DisplayName = "Nat vent max rel humidity")]
        public double NatVentMaxRelHumidity { get; set; }

        [SimulationSetting(DisplayName = "Nat vent schedule")]
        public YearSchedule NatVentSchedule { get; set; }

        [SimulationSetting(DisplayName = "Nat vent zone temperature setpoint")]
        public double NatVentZoneTempSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Scheduled ventilation")]
        public bool IsScheduledVentilationOn { get; set; }

        [SimulationSetting(DisplayName = "Scheduled ventilation ACH")]
        public double ScheduledVentilationAch { get; set; }

        [SimulationSetting(DisplayName = "Scheduled ventilation schedule")]
        public YearSchedule ScheduledVentilationSchedule { get; set; }

        [SimulationSetting(DisplayName = "Scheduled ventilation setpoint")]
        public double ScheduledVentilationSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Buoyancy")]
        public bool IsBuoyancyOn { get; set; }

        [SimulationSetting(DisplayName = "Wind")]
        public bool IsWindOn { get; set; }

        [SimulationSetting]
        public bool Afn { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            NatVentSchedule == component ||
            ScheduledVentilationSchedule == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneVentilation()
            {
                IsInfiltrationOn = IsInfiltrationOn,
                Infiltration = Infiltration,
                IsNatVentOn = IsNatVentOn,
                NatVentMinOutdoorAirTemp = NatVentMinOutdoorAirTemp,
                NatVentMaxOutdoorAirTemp = NatVentMaxOutdoorAirTemp,
                NatVentMaxRelHumidity = NatVentMaxRelHumidity,
                NatVentSchedule = NatVentSchedule,
                NatVentZoneTempSetpoint = NatVentZoneTempSetpoint,
                IsScheduledVentilationOn = IsScheduledVentilationOn,
                ScheduledVentilationAch = ScheduledVentilationAch,
                ScheduledVentilationSchedule = ScheduledVentilationSchedule,
                ScheduledVentilationSetpoint = ScheduledVentilationSetpoint,
                IsBuoyancyOn = IsBuoyancyOn,
                IsWindOn = IsWindOn,
                Afn = Afn,
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
