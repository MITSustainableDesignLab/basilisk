using Basilisk.Controls.Attributes;
using System.Collections.Generic;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.AdvancedStructuralModel))]
public class ConstructionSystemSettings
{
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFloor> Floors { get; set; }

    public IEnumerable<IConstructionSystem> All
    {
        get
        {
            yield return Floors;
        }
    }
}
