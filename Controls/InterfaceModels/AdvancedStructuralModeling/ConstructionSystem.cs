using System;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class ConstructionSystem<TConstructionSystemType> : IConstructionSystem
    where TConstructionSystemType : Enum
{
    public ConstructionSystem(string name)
    {
        Name = name;
    }

    public LibraryComponent Material { get; set; }

    public string Name { get; }

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
