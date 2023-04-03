using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract(IsReference = true)]
public class ConstructionSystem<TConstructionSystemType>
    where TConstructionSystemType : System.Enum
{
    [DataMember]
    public OpaqueMaterial? Material { get; set; }

    [DataMember]
    public TConstructionSystemType? ConstructionSystemType { get; set; }
}
