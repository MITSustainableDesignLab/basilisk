using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class YearSchedule : LibraryComponent
    {
        [DataMember]
        public override string Category
        {
            get { return "Year"; }
            set { /* I'd throw, but then deserialization wouldn't work */ }
        }

        [DataMember]
        public IList<YearSchedulePart> Parts { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            Parts.Select(part => part.Schedule);
    }
}
