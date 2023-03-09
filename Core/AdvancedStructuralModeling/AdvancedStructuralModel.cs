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
    public FoundationSoilSettings FoundationSoilSettings { get; set; } = new FoundationSoilSettings();

    [DataMember]
    public LoadingSettings LoadingSettings { get; set; } = new LoadingSettings();
}
