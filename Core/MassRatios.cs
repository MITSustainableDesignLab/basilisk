using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract]
    public class MassRatios
    {
        [DataMember]
        public double HighLoadRatio { get; set; }

        [DataMember]
        public OpaqueMaterial Material { get; set; }

        [DataMember]
        public double NormalRatio { get; set; }
    }
}
