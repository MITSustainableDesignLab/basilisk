using Basilisk.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum ConstructionSystemTypeFoundation
{
    [DisplayText("Spread footings")]
    SpreadFootings = 0,

    [DisplayText("Raft")]
    Raft,

    [DisplayText("Pile and pile caps")]
    PileAndPileCaps,

    [DisplayText("Shell footings")]
    ShellFootings
}
