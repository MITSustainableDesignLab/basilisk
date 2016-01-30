using System.ComponentModel;

using ArchsimLib;

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

        [SimulationSetting]
        public GlazingConstructionTypes Type { get; set; }
    }
}
