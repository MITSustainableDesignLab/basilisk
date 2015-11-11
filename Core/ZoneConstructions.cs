using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class ZoneConstructions : LibraryComponent
    {
        [DataMember]
        public OpaqueConstruction Facade { get; set; }

        [DataMember]
        public OpaqueConstruction Ground { get; set; }

        [DataMember]
        public OpaqueConstruction Partition { get; set; }

        [DataMember]
        public OpaqueConstruction Roof { get; set; }

        [DataMember]
        public OpaqueConstruction Slab { get; set; }

        [DataMember]
        public bool IsFacadeAdiabatic { get; set; }

        [DataMember]
        public bool IsGroundAdiabatic { get; set; }

        [DataMember]
        public bool IsPartitionAdiabatic { get; set; }

        [DataMember]
        public bool IsRoofAdiabatic { get; set; }

        [DataMember]
        public bool IsSlabAdiabatic { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    Facade,
                    Ground,
                    Partition,
                    Roof,
                    Slab
                }.Where(c => c != null);
                return direct.Concat(direct.SelectMany(c => c.ReferencedComponents));
            }
        }
    }
}
