using System;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class ConstructionSystem<TConstructionSystemType> : IMaterialPickable, INotifyPropertyChanged
    where TConstructionSystemType : Enum
{
    private LibraryComponent material;

    public ConstructionSystem(string name)
    {
        Name = name;
    }

    public event PropertyChangedEventHandler PropertyChanged;

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
}
