using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract]
    public class YearSchedulePart
    {
        [DataMember]
        public int FromDay { get; set; }

        [DataMember]
        public int FromMonth { get; set; }

        [DataMember]
        public int ToDay { get; set; }

        [DataMember]
        public int ToMonth { get; set; }

        [DataMember]
        public WeekSchedule Schedule { get; set; }
    }
}
