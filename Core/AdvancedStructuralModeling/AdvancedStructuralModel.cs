using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class AdvancedStructuralModel
{
    [DataMember]
    public ColumnWallSpacingSettings ColumnWallSpacing { get; set; } = new ColumnWallSpacingSettings();

    [DataMember]
    public ConstructionSystemSettings ConstructionSystems { get; set; } = new ConstructionSystemSettings();

    [DataMember]
    public LoadingSettings LoadingSettings { get; set; } = new LoadingSettings();
}
