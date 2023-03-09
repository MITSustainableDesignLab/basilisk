using Basilisk.Controls.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.AdvancedStructuralModel))]
public class AdvancedStructuralModel
{
    public ColumnWallSpacingSettings ColumnWallSpacing { get; set; }

    public ConstructionSystemSettings ConstructionSystems { get; set; }

    public FoundationSoilSettings FoundationSoilSettings { get; set; }

    public IEnumerable<FoundationSoilSettings> FoundationSoilSettingsList => Enumerable.Repeat(FoundationSoilSettings, 1);

    public LoadingSettings LoadingSettings { get; set; } = new LoadingSettings();

    public IEnumerable<LoadingSettings> LoadingSettingsList => Enumerable.Repeat(LoadingSettings, 1);
}
