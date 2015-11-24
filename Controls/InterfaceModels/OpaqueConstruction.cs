using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.OpaqueConstruction))]
    [DisplayName("opaque construction")]
    public class OpaqueConstruction : LayeredConstruction
    {
        public override LibraryComponent Duplicate()
        {
            var res = new OpaqueConstruction();
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
