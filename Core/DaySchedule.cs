using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class DaySchedule : LibraryComponent
    {
        [DataMember, DefaultValue("Fraction")]
        public string Type { get; set; }

        [DataMember]
        public IList<double> Values { get; set; }

        public static bool ValuesMatch(DaySchedule a, DaySchedule b)
        {
            if (a == null) { throw new ArgumentNullException("a"); }
            else if (b == null) { throw new ArgumentNullException("b"); }
            else if ((a.Values == null) != (b.Values == null)) { return false; }
            else if (a.Values == null) { return true; }
            IStructuralEquatable aValues = a.Values.ToArray();
            IStructuralEquatable bValues = b.Values.ToArray();
            return aValues.Equals(bValues, StructuralComparisons.StructuralEqualityComparer);
        }
    }
}
