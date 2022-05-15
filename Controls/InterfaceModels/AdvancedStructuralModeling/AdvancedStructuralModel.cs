using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.AdvancedStructuralModel))]
public class AdvancedStructuralModel
{
    public ColumnWallSpacingSettings ColumnWallSpacing { get; set; }
}
