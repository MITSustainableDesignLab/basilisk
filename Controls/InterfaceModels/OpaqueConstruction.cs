using System.ComponentModel;

using Basilisk.Controls.Attributes;

using ArchsimLib;
using System;

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

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (OpaqueConstruction)other;
            Type = c.Type;
            CopyBasePropertiesFrom(c, coord);
        }

        [SimulationSetting]
        public ConstructionTypes Type { get; set; }
    }
}
