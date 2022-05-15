using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class AdvancedStructuralModel
{
    [DataMember]
    public ColumnWallSpacingSettings ColumnWallSpacing { get; set; }

    //[DataMember]
    //public ConstructionSystemSettings ConstructionSystems { get; set; }
}
