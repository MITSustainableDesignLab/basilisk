﻿using Basilisk.Controls.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.ColumnWallSpacingSettings))]
public class ColumnWallSpacingSettings : LibraryComponent
{
    [SimulationSetting(DisplayName = "Primary span (x-direction)", Units = "m", MustBePositive = true)]
    public double PrimarySpan { get; set; }

    [SimulationSetting(DisplayName = "Secondary span (y-direction)", Units = "m", MustBePositive = true)]
    public double SecondarySpan { get; set; }

    public override IEnumerable<LibraryComponent> AllReferencedComponents =>
        Enumerable.Empty<LibraryComponent>();

    public override bool DirectlyReferences(LibraryComponent component) =>
        false;

    public override LibraryComponent Duplicate() =>
        new ColumnWallSpacingSettings
        {
            PrimarySpan = PrimarySpan,
            SecondarySpan = SecondarySpan,
        };

    public override void OverwriteWith(LibraryComponent other, ComponentCoordinator lookupFrom)
    {
        if (other is ColumnWallSpacingSettings cws)
        {
            cws.PrimarySpan = PrimarySpan;
            cws.SecondarySpan = SecondarySpan;
        }
    }
}
