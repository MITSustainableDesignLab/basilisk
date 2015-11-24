using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.OpaqueMaterial))]
    [DisplayName("opaque material")]
    public class OpaqueMaterial : MaterialBase
    {
        [SimulationSetting(DisplayName = "Solar Absorptance")]
        public double SolarAbsorptance { get; set; }

        [SimulationSetting(DisplayName = "Specific Heat")]
        public double SpecificHeat { get; set; }

        [SimulationSetting(DisplayName = "Thermal Emittance")]
        public double ThermalEmittance { get; set; }

        [SimulationSetting(DisplayName = "Visible Absorptance")]
        public double VisibleAbsorptance { get; set; }

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
