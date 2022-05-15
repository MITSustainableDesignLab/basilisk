using System;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class ConstructionSystem<TConstructionSystemType> : IConstructionSystem
    where TConstructionSystemType : Enum
{
    public LibraryComponent Material { get; set; }

    public TConstructionSystemType ConstructionSystemType { get; set; }

    Enum IConstructionSystem.ConstructionSystemType
    {
        get => ConstructionSystemType;
        set
        {
            if (value is TConstructionSystemType t)
            {
                ConstructionSystemType = t;
            }
        }
    }
}
