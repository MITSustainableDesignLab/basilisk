using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class ZoneVentilation : LibraryComponent
    {
        [DataMember]
        public bool Afn { get; set; }

        [DataMember, DefaultValue(true)]
        public bool IsBuoyancyOn { get; set; } = true;

        [DataMember, DefaultValue(0.1)]
        public double Infiltration { get; set; } = 0.1;

        [DataMember, DefaultValue(true)]
        public bool IsInfiltrationOn { get; set; } = true;

        [DataMember]
        public bool IsNatVentOn { get; set; }

        [DataMember]
        public bool IsScheduledVentilationOn { get; set; }

        [DataMember, DefaultValue(90)]
        public double NatVentMaxRelHumidity { get; set; } = 90;

        [DataMember, DefaultValue(30)]
        public double NatVentMaxOutdoorAirTemp { get; set; } = 30;

        [DataMember, DefaultValue(0)]
        public double NatVentMinOutdoorAirTemp { get; set; } = 0;

        [DataMember]
        public YearSchedule? NatVentSchedule { get; set; }

        [DataMember, DefaultValue(18)]
        public double NatVentZoneTempSetpoint { get; set; } = 18;

        [DataMember, DefaultValue(0.6)]
        public double ScheduledVentilationAch { get; set; } = 0.6;

        [DataMember]
        public YearSchedule? ScheduledVentilationSchedule { get; set; }

        [DataMember, DefaultValue(18)]
        public double ScheduledVentilationSetpoint { get; set; } = 18;

        [DataMember]
        public bool IsWindOn { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            new LibraryComponent?[]
            {
                NatVentSchedule,
                ScheduledVentilationSchedule
            }.OfType<LibraryComponent>();
    }
}
