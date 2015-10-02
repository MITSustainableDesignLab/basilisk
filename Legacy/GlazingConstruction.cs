using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public class GlazingConstruction : BaseConstruction<GlazingLayer>
    {
        public GlazingConstruction() { Layers = new List<GlazingLayer>(); }

        public override List<GlazingLayer> Layers { get; set; }
    }
}
