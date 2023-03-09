using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum FoundationSoilPreset
{
    Clay = 0,
    Gravel,
    Sand,
    Other
}
