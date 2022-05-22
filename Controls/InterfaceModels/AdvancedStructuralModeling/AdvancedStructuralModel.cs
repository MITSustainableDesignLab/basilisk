using Basilisk.Controls.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.AdvancedStructuralModel))]
public class AdvancedStructuralModel
{
    public ColumnWallSpacingSettings ColumnWallSpacing { get; set; }

    public ConstructionSystemSettings ConstructionSystems { get; set; }

    public LoadingSettings LoadingSettings { get; set; } = new LoadingSettings();

    public IEnumerable<LoadingSettings> LoadingSettingsList => Enumerable.Repeat(LoadingSettings, 1);
}
