using Basilisk.Core.AdvancedStructuralModeling;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class StructureInformation : ConstructionBase
    {
        [DataMember]
        public AdvancedStructuralModel AdvancedModel { get; set; } = new AdvancedStructuralModel();

        [DataMember]
        public ICollection<MassRatios> MassRatios { get; set; } = new List<MassRatios>();

        [DataMember]
        public bool UseAdvancedModel { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            MassRatios.Select(m => m.Material).OfType<LibraryComponent>();
    }
}
