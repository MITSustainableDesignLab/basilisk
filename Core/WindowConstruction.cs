using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class WindowConstruction : LayeredConstruction<WindowMaterialBase>
    {
        public WindowConstruction()
        {
            Layers = new List<MaterialLayer<WindowMaterialBase>>();
        }

        [DataMember]
        public override IList<MaterialLayer<WindowMaterialBase>> Layers { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1} layer{2})", Name, Layers.Count, Layers.Count == 1 ? "" : "s");
        }
    }
}
