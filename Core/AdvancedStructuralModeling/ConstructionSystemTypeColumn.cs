using Basilisk.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum ConstructionSystemTypeColumn
{
    [DisplayText("Square solid")]
    SquareSolid = 0,

    [DisplayText("I-beam")]
    IBeam,

    [DisplayText("Square tube")]
    SquareTube
}
