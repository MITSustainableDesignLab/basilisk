using Basilisk.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum ConstructionSystemTypeLateralSystem
{
    [DisplayText("Cross-bracing")]
    CrossBracing = 0,

    [DisplayText("Moment frames")]
    MomentFrames,

    [DisplayText("Shear walls")]
    ShearWalls
}
