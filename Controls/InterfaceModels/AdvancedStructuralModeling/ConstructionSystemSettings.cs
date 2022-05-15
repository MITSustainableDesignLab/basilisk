using Basilisk.Controls.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.AdvancedStructuralModel))]
public class ConstructionSystemSettings
{
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFloor> Floors { get; set; }

    public IEnumerable<INotifyPropertyChanged> All
    {
        get
        {
            yield return Floors;
        }
    }
}
