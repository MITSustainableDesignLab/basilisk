using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class ConstructionSystemSettings
{
    public ConstructionSystem<ConstructionSystemTypeFloor> Floors { get; set; }
}
