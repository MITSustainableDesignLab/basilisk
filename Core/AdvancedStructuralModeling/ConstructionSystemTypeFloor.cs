using Basilisk.Core.Attributes;

namespace Basilisk.Core.AdvancedStructuralModeling;

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
