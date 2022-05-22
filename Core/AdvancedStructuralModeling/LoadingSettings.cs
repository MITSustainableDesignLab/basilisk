using System.ComponentModel;
using System.Runtime.Serialization;

namespace Basilisk.Core.AdvancedStructuralModeling;

[DataContract]
public class LoadingSettings
{
    [DataMember]
    [DefaultValue(LiveLoadingPreset.Offices)]
    public LiveLoadingPreset LiveLoadingPreset { get; set; } = LiveLoadingPreset.Offices;

    [DataMember]
    [DefaultValue(2.4)]
    public double LiveLoadingValue { get; set; } = 2.4;
}
