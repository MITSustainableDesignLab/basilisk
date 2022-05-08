using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core;

[DataContract(IsReference = false)]
public class AdvancedStructuralModel
{
    [DataMember]
    public ColumnWallSpacingSettings ColumnWallSpacing { get; set; }

    [DataContract(IsReference = false)]
    public class ColumnWallSpacingSettings
    {
        [DataMember, DefaultValue(8.0)]
        public double PrimarySpan { get; set; } = 8.0;

        [DataMember, DefaultValue(8.0)]
        public double SecondarySpan { get; set; } = 8.0;
    }
}
