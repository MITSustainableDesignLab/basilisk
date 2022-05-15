using System;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public interface IConstructionSystem : IMaterialPickable
{
    public Enum ConstructionSystemType { get; set; }
}
