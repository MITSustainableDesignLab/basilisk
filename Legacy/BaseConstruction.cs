using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public abstract class BaseConstruction<LayerT> where LayerT : BaseLayer
    {
        public double AssemblyCarbon { get; set; }
        public double AssemblyCost { get; set; }
        public double AssemblyEnergy { get; set; }
        public string Comments { get; set; }
        public string DataSource { get; set; }
        public double DisassemblyCarbon { get; set; }
        public double DisassemblyEnergy { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public abstract List<LayerT> Layers { get; set; }
    }
}
