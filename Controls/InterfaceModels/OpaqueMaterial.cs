using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.OpaqueMaterial))]
    public class OpaqueMaterial : MaterialBase
    {
        static OpaqueMaterial()
        {
            Mapper
                .CreateMap<Core.OpaqueMaterial, OpaqueMaterial>()
                .IncludeBase<Core.MaterialBase, MaterialBase>();
        }

        [SimulationSetting(DisplayName = "Solar Absorptance")]
        public double SolarAbsorptance { get; set; }

        [SimulationSetting(DisplayName = "Specific Heat")]
        public double SpecificHeat { get; set; }

        [SimulationSetting(DisplayName = "Thermal Emittance")]
        public double ThermalEmittance { get; set; }

        [SimulationSetting(DisplayName = "Visible Absorptance")]
        public double VisibleAbsorptance { get; set; }
    }
}
