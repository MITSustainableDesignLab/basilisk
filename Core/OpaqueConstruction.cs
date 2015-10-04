using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class OpaqueConstruction : LayeredConstruction<OpaqueMaterial>
    {
        public OpaqueConstruction()
        {
            Layers = new List<MaterialLayer<OpaqueMaterial>>();
        }

        [DataMember]
        public override IList<MaterialLayer<OpaqueMaterial>> Layers { get; set; }
    }
}
