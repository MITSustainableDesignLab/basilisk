using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class FoundationSoilSettings
{
    [DataMember]
    [DefaultValue(FoundationSoilPreset.Clay)]
    public FoundationSoilPreset FoundationSoilPreset { get; set; } = FoundationSoilPreset.Clay;

    [DataMember]
    [DefaultValue(72)]
    public double FoundationSoilValue { get; set; } = 72;
}
