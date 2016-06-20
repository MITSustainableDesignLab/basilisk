using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class StructureInformation : ConstructionBase
    {
        [DataMember]
        public ICollection<MassRatios> MassRatios { get; set; } = new List<MassRatios>();

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            MassRatios.Select(m => m.Material);
    }
}
