using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.GlazingMaterial))]
    [DisplayName("glazing material")]
    [ComponentNamespace]
    public class GlazingMaterial : WindowMaterialBase
    {
        [SimulationSetting(DisplayName = "Dirt Factor")]
        [DefaultValue(1)]
        public double DirtFactor { get; set; } = 1;

        [SimulationSetting(DisplayName = "Back-side IR Emissivity")]
        [DefaultValue(0.84)]
        public double IREmissivityBack { get; set; } = 0.84;

        [SimulationSetting(DisplayName = "Front-side IR Emissivity")]
        [DefaultValue(0.84)]
        public double IREmissivityFront { get; set; } = 0.84;

        [SimulationSetting(DisplayName = "IR Transmittance")]
        [DefaultValue(0.0)]
        public double IRTransmittance { get; set; } = 0.0;

        [SimulationSetting(DisplayName = "Back-side Solar Reflectance")]
        [DefaultValue(0.075)]
        public double SolarReflectanceBack { get; set; } = 0.075;

        [SimulationSetting(DisplayName = "Front-side Solar Reflectance")]
        [DefaultValue(0.075)]
        public double SolarReflectanceFront { get; set; } = 0.075;

        [SimulationSetting(DisplayName = "Solar Transmittance")]
        [DefaultValue(0.837)]
        public double SolarTransmittance { get; set; } = 0.837;

        [SimulationSetting(DisplayName = "Back-side Visible Reflectance")]
        [DefaultValue(0.081)]
        public double VisibleReflectanceBack { get; set; } = 0.081;

        [SimulationSetting(DisplayName = "Front-side Visible Reflectance")]
        [DefaultValue(0.081)]
        public double VisibleReflectanceFront { get; set; } = 0.081;

        [SimulationSetting(DisplayName = "Visible Transmittance")]
        [DefaultValue(0.898)]
        public double VisibleTransmittance { get; set; } = 0.898;

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
