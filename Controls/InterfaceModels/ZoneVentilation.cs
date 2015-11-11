using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneVentilation))]
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

        [SimulationSetting(DisplayName = "Ventilation")]
        public bool IsVentOn { get; set; }

        [SimulationSetting(DisplayName = "Ventilation ACH")]
        public double VentAirChangesPerHour { get; set; }

        [SimulationSetting(DisplayName = "Ventilation schedule")]
        public YearSchedule VentSchedule { get; set; }

        [SimulationSetting(DisplayName = "Schedule ventilation min temp")]
        public double ScheduleVentMinTemp { get; set; }

        [SimulationSetting]
        public bool Buoy { get; set; }

        [SimulationSetting]
        public bool Wind { get; set; }

        [SimulationSetting]
        public bool Afn { get; set; }

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
                IsVentOn = IsVentOn,
                VentAirChangesPerHour = VentAirChangesPerHour,
                VentSchedule = VentSchedule,
                ScheduleVentMinTemp = ScheduleVentMinTemp,
                Buoy = Buoy,
                Wind = Wind,
                Afn = Afn,
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
