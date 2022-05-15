using Basilisk.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum ConstructionSystemTypeBeam
{
    [DisplayText("Rectangular solid")]
    RectangularSolid = 0,

    [DisplayText("I-beam")]
    IBeam,

    [DisplayText("Rectangular tube")]
    RectangularTube,

    [DisplayText("Truss")]
    Truss
}
