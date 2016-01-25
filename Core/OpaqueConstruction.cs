using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using ArchsimLib;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class OpaqueConstruction : LayeredConstruction<OpaqueMaterial>
    {
        [DataMember]
        public override IList<MaterialLayer<OpaqueMaterial>> Layers { get; set; } = new List<MaterialLayer<OpaqueMaterial>>();

        [DataMember]
        public ConstructionTypes Type { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
