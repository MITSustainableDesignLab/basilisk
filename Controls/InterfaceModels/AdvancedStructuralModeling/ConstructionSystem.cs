using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class ConstructionSystem<TConstructionSystemType> : IMaterialPickable, INotifyPropertyChanged
    where TConstructionSystemType : Enum
{
    private TConstructionSystemType constructionSystemType;
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

    public TConstructionSystemType ConstructionSystemType
    {
        get => constructionSystemType;
        set
        {
            if (!EqualityComparer<TConstructionSystemType>.Default.Equals(constructionSystemType, value))
            {
                constructionSystemType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConstructionSystemType)));
            }
        }
    }
}
