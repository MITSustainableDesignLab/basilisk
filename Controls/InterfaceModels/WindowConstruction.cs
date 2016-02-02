using System;
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
            var res = new WindowConstruction()
            {
                Type = Type
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (WindowConstruction)other;
            Type = c.Type;
            CopyBasePropertiesFrom(c, coord);
        }

        [SimulationSetting]
        public GlazingConstructionTypes Type { get; set; }
    }
}
