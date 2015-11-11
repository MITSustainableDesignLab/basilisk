using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class ZoneLoads : LibraryComponent
    {
        [DataMember, DefaultValue("Continuous")]
        public string DimmingType { get; set; } = "Continuous";

        [DataMember]
        public YearSchedule EquipmentAvailabilitySchedule { get; set; }

        [DataMember, DefaultValue(12)]
        public double EquipmentPowerDensity { get; set; } = 12;

        [DataMember, DefaultValue(500)]
        public double IlluminanceTarget { get; set; } = 500;

        [DataMember, DefaultValue(12)]
        public double LightingPowerDensity { get; set; } = 12;

        [DataMember]
        public YearSchedule LightsAvailabilitySchedule { get; set; }

        [DataMember]
        public YearSchedule OccupancySchedule { get; set; }

        [DataMember, DefaultValue(true)]
        public bool IsEquipmentOn { get; set; } = true;

        [DataMember, DefaultValue(true)]
        public bool IsLightingOn { get; set; } = true;

        [DataMember, DefaultValue(true)]
        public bool IsPeopleOn { get; set; } = true;

        [DataMember, DefaultValue(0.2)]
        public double PeopleDensity { get; set; } = 0.2;

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            new LibraryComponent[]
            {
                EquipmentAvailabilitySchedule,
                LightsAvailabilitySchedule,
                OccupancySchedule
            }.Where(s => s != null);
    }
}
