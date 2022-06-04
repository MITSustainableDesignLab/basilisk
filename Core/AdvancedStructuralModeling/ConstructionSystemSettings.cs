using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class ConstructionSystemSettings
{
    [DataMember]
    public ConstructionSystem<ConstructionSystemTypeBeam> Beams { get; set; } = new();


    [DataMember]
    public ConstructionSystem<ConstructionSystemTypeColumn> Columns { get; set; } = new();

    [DataMember]
    public ConstructionSystem<ConstructionSystemTypeFloor> Floors { get; set; } = new();

    [DataMember]
    public ConstructionSystem<ConstructionSystemTypeFoundation> Foundations { get; set; } = new();

    [DataMember]
    public ConstructionSystem<ConstructionSystemTypeLateralSystem> LateralSystem { get; set; } = new();
}
