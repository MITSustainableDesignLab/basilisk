﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(ArchsimLib.OpaqueMaterial))]
    [DisplayName("opaque material")]
    [ComponentNamespace]
    public class OpaqueMaterial : MaterialBase
    {
        [SimulationSetting]
        public double MoistureDiffusionResistance { get; set; }

        [SimulationSetting]
        [DefaultValue("")]
        public string Roughness { get; set; }

        [SimulationSetting(DisplayName = "Solar Absorptance")]
        public double SolarAbsorptance { get; set; }

        [SimulationSetting(DisplayName = "Specific Heat")]
        public double SpecificHeat { get; set; }

        [SimulationSetting(DisplayName = "Thermal Emittance")]
        public double ThermalEmittance { get; set; }

        [SimulationSetting(DisplayName = "Visible Absorptance")]
        public double VisibleAbsorptance { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new OpaqueMaterial()
            {
                SolarAbsorptance = SolarAbsorptance,
                SpecificHeat = SpecificHeat,
                ThermalEmittance = ThermalEmittance,
                VisibleAbsorptance = VisibleAbsorptance
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
