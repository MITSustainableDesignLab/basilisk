using System.ComponentModel;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WindowConstruction))]
    [DisplayName("window construction")]
    [ComponentNamespace]
    public class WindowConstruction : LayeredConstruction
    {
        public override LibraryComponent Duplicate()
        {
            var res = new WindowConstruction();
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (WindowConstruction)other;
            CopyBasePropertiesFrom(c, coord);
        }
    }
}
