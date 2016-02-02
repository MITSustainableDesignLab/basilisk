using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(ArchsimLib.GlazingMaterial))]
    [DisplayName("glazing material")]
    [ComponentNamespace]
    public class GlazingMaterial : WindowMaterialBase
    {
        [SimulationSetting(DisplayName = "Dirt Factor")]
        public double DirtFactor { get; set; }

        [SimulationSetting(DisplayName = "Back-side IR Emissivity")]
        public double IREmissivityBack { get; set; }

        [SimulationSetting(DisplayName = "Front-side IR Emissivity")]
        public double IREmissivityFront { get; set; }

        [SimulationSetting(DisplayName = "IR Transmittance")]
        public double IRTransmittance { get; set; }

        [SimulationSetting(DisplayName = "Back-side Solar Reflectance")]
        public double SolarReflectanceBack { get; set; }

        [SimulationSetting(DisplayName = "Front-side Solar Reflectance")]
        public double SolarReflectanceFront { get; set; }

        [SimulationSetting(DisplayName = "Solar Transmittance")]
        public double SolarTransmittance { get; set; }

        [SimulationSetting(DisplayName = "Back-side Visible Reflectance")]
        public double VisibleReflectanceBack { get; set; }

        [SimulationSetting(DisplayName = "Front-side Visible Reflectance")]
        public double VisibleReflectanceFront { get; set; }

        [SimulationSetting(DisplayName = "Visible Transmittance")]
        public double VisibleTransmittance { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new GlazingMaterial();
            CopyProperties(res, this);
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator _)
        {
            var c = (GlazingMaterial)other;
            CopyProperties(this, c);
            CopyBasePropertiesFrom(c);
        }

        private static void CopyProperties(GlazingMaterial to, GlazingMaterial from)
        {
            to.DirtFactor = from.DirtFactor;
            to.IREmissivityBack = from.IREmissivityBack;
            to.IREmissivityFront = from.IREmissivityFront;
            to.IRTransmittance = from.IRTransmittance;
            to.SolarReflectanceBack = from.SolarReflectanceBack;
            to.SolarReflectanceFront = from.SolarReflectanceFront;
            to.SolarTransmittance = from.SolarTransmittance;
            to.VisibleReflectanceBack = from.VisibleReflectanceBack;
            to.VisibleReflectanceFront = from.VisibleReflectanceFront;
            to.VisibleTransmittance = from.VisibleTransmittance;
        }
    }
}
