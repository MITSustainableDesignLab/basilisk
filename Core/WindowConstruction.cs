using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using ArchsimLib;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class WindowConstruction : LayeredConstruction<WindowMaterialBase>
    {
        [DataMember]
        public override IList<MaterialLayer<WindowMaterialBase>> Layers { get; set; } = new List<MaterialLayer<WindowMaterialBase>>();

        [DataMember]
        public GlazingConstructionTypes Type { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
