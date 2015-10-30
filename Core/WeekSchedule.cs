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
        public override string Category
        {
            get { return "Week"; }
            set { /* I'd throw, but then deserialization wouldn't work */ }
        }

        [DataMember]
        public DaySchedule[] Days { get; set; }

        [DataMember, DefaultValue("Fraction")]
        public string Type { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents => Days;
    }
}
