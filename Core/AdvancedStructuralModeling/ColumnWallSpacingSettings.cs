using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class ColumnWallSpacingSettings
{
    [DataMember, DefaultValue(8.0)]
    public double PrimarySpan { get; set; } = 8.0;

    [DataMember, DefaultValue(8.0)]
    public double SecondarySpan { get; set; } = 8.0;
}
