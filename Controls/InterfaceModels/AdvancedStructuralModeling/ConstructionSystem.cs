namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class ConstructionSystem<TConstructionSystemType>
    where TConstructionSystemType : System.Enum
{
    public LibraryComponent Material { get; set; }

    public TConstructionSystemType ConstructionSystemType { get; set; }
}
