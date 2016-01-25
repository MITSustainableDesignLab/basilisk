using Basilisk.Controls.Attributes;

using ArchsimLib;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.OpaqueConstruction))]
    [DisplayName("opaque construction")]
    [ComponentNamespace]
    public class OpaqueConstruction : LayeredConstruction
    {
        public override LibraryComponent Duplicate()
        {
            var res = new OpaqueConstruction();
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        [SimulationSetting]
        public ConstructionTypes Type { get; set; }
    }
}
