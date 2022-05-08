using Basilisk.Controls.InterfaceModels;
using System;
using System.Collections.Generic;

namespace Basilisk.Controls
{
    public interface IComponentCoordinator
    {
        IEnumerable<LibraryComponent> ComponentsOfType(Type type);
    }
}