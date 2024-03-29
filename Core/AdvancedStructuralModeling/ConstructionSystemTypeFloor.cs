﻿using Basilisk.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum ConstructionSystemTypeFloor
{
    [DisplayText("One-way flat slab")]
    OneWayFlatSlab = 0,

    [DisplayText("One-way ribbed slab")]
    OneWayRibbedSlab,

    [DisplayText("One-way slab on metal deck")]
    OneWaySlabOnMetalDeck,

    [DisplayText("One-way joist system")]
    OneWayJoistSystem,

    [DisplayText("Two-way flat slab")]
    TwoWayFlatSlab,

    [DisplayText("Two-way waffle slab")]
    TwoWayWaffleSlab
}
