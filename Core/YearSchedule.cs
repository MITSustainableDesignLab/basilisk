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
        public IList<YearSchedulePart> Parts { get; set; }
    }
}
