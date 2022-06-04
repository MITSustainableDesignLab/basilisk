using Basilisk.Controls.InterfaceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basilisk.Controls;

internal class NullComponentCoordinator : IComponentCoordinator
{
    public IEnumerable<LibraryComponent> ComponentsOfType(Type type) =>
        Enumerable.Empty<LibraryComponent>();
}
