using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.OpaqueMaterial))]
    [DisplayName("opaque material")]
    [ComponentNamespace]
    public class OpaqueMaterial : MaterialBase
    {
        [SimulationSetting(DisplayName = "Moisture Diffusion Resistance")]
        public double MoistureDiffusionResistance { get; set; } = 50;

        [SimulationSetting]
        [DefaultValue("Rough")]
        public string Roughness { get; set; } = "Rough";

        [SimulationSetting(DisplayName = "Solar Absorptance")]
        [DefaultValue(0.7)]
        public double SolarAbsorptance { get; set; } = 0.7;

        [SimulationSetting(DisplayName = "Specific Heat", Units = "J/kgK")]
        [DefaultValue(840)]
        public double SpecificHeat { get; set; } = 840;

        [SimulationSetting(DisplayName = "Thermal Emittance")]
        [DefaultValue(0.9)]
        public double ThermalEmittance { get; set; } = 0.9;

        [SimulationSetting(DisplayName = "Visible Absorptance")]
        [DefaultValue(0.7)]
        public double VisibleAbsorptance { get; set; } = 0.7;

        [SimulationSetting(DisplayName = "Design Strength", Units = "MPa")]
        public double? DesignStrength { get; set; }

        [SimulationSetting(DisplayName = "Modulus of Elasticity", Units = "MPa")]
        public double? ModulusOfElasticity { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new OpaqueMaterial();
            CopyProperties(res, this);
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator _)
        {
            var c = (OpaqueMaterial)other;
            CopyProperties(this, c);
            CopyBasePropertiesFrom(c);
        }

        private static void CopyProperties(OpaqueMaterial to, OpaqueMaterial from)
        {
            to.Roughness = from.Roughness;
            to.SolarAbsorptance = from.SolarAbsorptance;
            to.SpecificHeat = from.SpecificHeat;
            to.ThermalEmittance = from.ThermalEmittance;
            to.VisibleAbsorptance = from.VisibleAbsorptance;
            to.MoistureDiffusionResistance = from.MoistureDiffusionResistance;
            to.DesignStrength = from.DesignStrength;
            to.ModulusOfElasticity = from.ModulusOfElasticity;
        }
    }
}
