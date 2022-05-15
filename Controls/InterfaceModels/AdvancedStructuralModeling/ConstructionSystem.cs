using System;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class ConstructionSystem<TConstructionSystemType> : IConstructionSystem, INotifyPropertyChanged
    where TConstructionSystemType : Enum
{
    private LibraryComponent material;

    public ConstructionSystem(string name)
    {
        Name = name;
    }

    public LibraryComponent Material
    {
        get => material;
        set
        {
            if (material != value)
            {
                material = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Material)));
            }
        }
    }

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

    public event PropertyChangedEventHandler PropertyChanged;
}
