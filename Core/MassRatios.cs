using System.Runtime.Serialization;

using ArchsimLib;

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
