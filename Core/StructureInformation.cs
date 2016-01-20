using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using ArchsimLib;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class StructureInformation : ConstructionBase
    {
        [DataMember]
        public ICollection<MassRatios> MassRatios { get; set; } = new List<MassRatios>();

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
