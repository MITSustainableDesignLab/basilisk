using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class WeekSchedule : LibraryComponent
    {
        [DataMember]
        public DaySchedule[] Days { get; set; }

        [DataMember, DefaultValue("Fraction")]
        public string Type { get; set; }
    }
}
