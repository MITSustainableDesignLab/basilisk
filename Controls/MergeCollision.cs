using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public struct MergeCollision
    {
        public MergeCollision(LibraryComponent originalComponent, LibraryComponent newComponent)
        {
            NewComponent = newComponent;
            OriginalComponent = originalComponent;
        }

        public LibraryComponent NewComponent { get; }
        public LibraryComponent OriginalComponent { get; }
    }
}
