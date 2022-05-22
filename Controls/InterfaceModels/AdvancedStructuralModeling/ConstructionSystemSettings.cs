using Basilisk.Controls.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

[UseDefaultValuesOf(typeof(Core.AdvancedStructuralModeling.AdvancedStructuralModel))]
public class ConstructionSystemSettings
{
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeBeam> Beams { get; set; }
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeColumn> Columns { get; set; }
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFloor> Floors { get; set; }
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFoundation> Foundations { get; set; }
    public ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeLateralSystem> LateralSystem { get; set; }

    public IEnumerable<INotifyPropertyChanged> All
    {
        get
        {
            yield return Floors;
            yield return Beams;
            yield return Columns;
            yield return LateralSystem;
            yield return Foundations;
        }
    }

    public IEnumerable<LibraryComponent> AllReferencedComponents
    {
        get
        {
            if (Beams.Material is not null)
            {
                yield return Beams.Material;
            }

            if (Columns.Material is not null)
            {
                yield return Columns.Material;
            }

            if (Floors.Material is not null)
            {
                yield return Floors.Material;
            }

            if (Foundations.Material is not null)
            {
                yield return Foundations.Material;
            }

            if (LateralSystem.Material is not null)
            {
                yield return LateralSystem.Material;
            }

        }
    }
}
