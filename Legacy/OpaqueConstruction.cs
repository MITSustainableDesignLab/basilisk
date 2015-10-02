using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public class OpaqueConstruction : BaseConstruction<OpaqueLayer>
    {
        public OpaqueConstruction() {  Layers = new List<OpaqueLayer>();}

        public override List<OpaqueLayer> Layers { get; set; }
    }
}
