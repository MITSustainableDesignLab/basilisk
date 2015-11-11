using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class WindowSettings : LibraryComponent
    {
        [DataMember, DefaultValue(0.65)]
        public double AfnDischargeC { get; set; } = 0.65;

        [DataMember, DefaultValue(20)]
        public double AfnTempSetpoint { get; set; } = 20;

        [DataMember]
        public YearSchedule AfnWindowAvailbility { get; set; }

        [DataMember]
        public WindowConstruction Construction { get; set; }

        [DataMember, DefaultValue(true)]
        public bool IsShadingSystemOn { get; set; }

        [DataMember]
        public bool IsVirtualPartition { get; set; }

        [DataMember]
        public bool IsZoneMixingOn { get; set; }

        [DataMember, DefaultValue(0.8)]
        public double OperableArea { get; set; } = 0.8;

        [DataMember]
        public YearSchedule ShadingSystemAvailabilitySchedule { get; set; }

        [DataMember, DefaultValue(180)]
        public double ShadingSystemSetpoint { get; set; } = 180;

        [DataMember, DefaultValue(0.5)]
        public double ShadingSystemTransmittance { get; set; } = 0.5;

        [DataMember, DefaultValue("ExteriorShade")]
        public string ShadingSystemType { get; set; } = "ExteriorShade";

        [DataMember, DefaultValue(WindowType.External)]
        public WindowType Type { get; set; } = WindowType.External;

        [DataMember]
        public YearSchedule ZoneMixingAvailabilitySchedule { get; set; }

        [DataMember, DefaultValue(2.0)]
        public double ZoneMixingDeltaTemperature { get; set; } = 2.0;

        [DataMember, DefaultValue(0.001)]
        public double ZoneMixingFlowRate { get; set; } = .001;

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    AfnWindowAvailbility,
                    Construction,
                    ShadingSystemAvailabilitySchedule,
                    ZoneMixingAvailabilitySchedule,
                }.Where(c => c != null);
                return direct.Concat(direct.SelectMany(c => c.ReferencedComponents));
            }
        }
    }
}
