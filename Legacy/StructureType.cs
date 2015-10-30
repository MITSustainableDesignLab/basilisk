using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public class StructureType
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DataSource { get; set; }
        public string Comments { get; set; }

        public string VerticalSystem { get; set; }
        public string HorizontalSystem { get; set; }
        public string LateralSystem { get; set; }
        public bool FoundationIn { get; set; }

        public List<MaterialComp> Materials { get; set; }

        public int MaxFloorNum { get; set; }
        public double MaxWWRatio { get; set; }
        public double MaxAveSpan { get; set; }

        public double AssemblyEnergy { get; set; }
        public double DisassemblyEnergy { get; set; }
        public double AssemblyCarbon { get; set; }
        public double DisassemblyCarbon { get; set; }
        public double AssemblyCost { get; set; }
    }
}
