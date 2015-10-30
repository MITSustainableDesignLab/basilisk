using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.GlazingMaterial))]
    public class GlazingMaterial : WindowMaterialBase
    {
        static GlazingMaterial()
        {
            Mapper
                .CreateMap<Core.GlazingMaterial, GlazingMaterial>()
                .IncludeBase<Core.MaterialBase, MaterialBase>();
        }

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

        public override LibraryComponent Duplicate()
        {
            var res = new GlazingMaterial()
            {
                DirtFactor = DirtFactor,
                IREmissivityBack = IREmissivityBack,
                IREmissivityFront = IREmissivityFront,
                IRTransmittance = IRTransmittance,
                SolarReflectanceBack = SolarReflectanceBack,
                SolarReflectanceFront = SolarReflectanceFront,
                SolarTransmittance = SolarTransmittance,
                VisibleReflectanceBack = VisibleReflectanceBack,
                VisibleReflectanceFront = VisibleReflectanceFront,
                VisibleTransmittance = VisibleTransmittance
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
