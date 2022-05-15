using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class ConstructionSystemSettings
{
    [DataMember]
    public ConstructionSystem<ConstructionSystemTypeFloor> Floors { get; set; } = new ConstructionSystem<ConstructionSystemTypeFloor>();
}
