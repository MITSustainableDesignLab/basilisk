using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    public abstract class MaterialBase : LibraryComponent
    {
        [SimulationSetting(Units = "W/mK")]
        [DefaultValue(2.4)]
        public double Conductivity { get; set; } = 2.4;

        [SimulationSetting]
        public double Cost { get; set; }

        [SimulationSetting(Units = "kg/m3")]
        [DefaultValue(2400)]
        public double Density { get; set; } = 2400;

        [SimulationSetting(DisplayName = "Embodied Carbon", Units = "kgCO2/kg")]
        public double EmbodiedCarbon { get; set; }

        [SimulationSetting(DisplayName = "Embodied Energy", Units = "MJ/kg")]
        public double EmbodiedEnergy { get; set; }

        [SimulationSetting(DisplayName = "Substitution Rate Pattern")]
        [DefaultValue(new double[] { 1.0 })]
        public double[] SubstitutionRatePattern { get; set; }

        [SimulationSetting(DisplayName = "Substitution Timestep")]
        public double SubstitutionTimestep { get; set; }

        [SimulationSetting(DisplayName = "Transportation Carbon", Units = "kgCO2/kg/km")]
        public double TransportCarbon { get; set; }

        [SimulationSetting(DisplayName = "Transportation Distance", Units = "km")]
        public double TransportDistance { get; set; }

        [SimulationSetting(DisplayName = "Transportation Energy", Units = "MJ/kg/km")]
        public double TransportEnergy { get; set; }

        protected void CopyBasePropertiesFrom(MaterialBase source)
        {
            Conductivity = source.Conductivity;
            Cost = source.Cost;
            Density = source.Density;
            EmbodiedCarbon = source.EmbodiedCarbon;
            EmbodiedEnergy = source.EmbodiedEnergy;
            SubstitutionRatePattern = source.SubstitutionRatePattern.ToArray();
            SubstitutionTimestep = source.SubstitutionTimestep;
            TransportCarbon = source.TransportCarbon;
            TransportDistance = source.TransportDistance;
            TransportEnergy = source.TransportEnergy;
            CopyBasePropertiesFrom((LibraryComponent)source);
        }
    }
}
