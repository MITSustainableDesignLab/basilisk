using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Core;

namespace Basilisk.LibraryEditor.ViewModels
{
    internal class DesignExample
    {
        public ObservableCollection<LibraryComponent> LoadedOpaqueMaterials =>
            new ObservableCollection<LibraryComponent>()
            {
                new OpaqueMaterial() { Name = "Material 1", Category = "Category 1" },
                new OpaqueMaterial() { Name = "Material 2", Category = "Category 2"  },
                new OpaqueMaterial() { Name = "Material 3", Category = "Category 3"  }
            };

        public string CurrentLibraryPath => null;
        public bool HasUnsavedChanges => true;
        public bool IsAnyLibraryLoaded => true;
    }
}
