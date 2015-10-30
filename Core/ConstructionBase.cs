using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public abstract class ConstructionBase : LibraryComponent
    {
        [DataMember]
        public double AssemblyCarbon { get; set; }

        [DataMember]
        public double AssemblyCost { get; set; }

        [DataMember]
        public double AssemblyEnergy { get; set; }

        [DataMember]
        public double DisassemblyCarbon { get; set; }

        [DataMember]
        public double DisassemblyEnergy { get; set; }
    }
}
