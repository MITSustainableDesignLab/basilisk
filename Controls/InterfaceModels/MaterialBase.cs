using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    public abstract class MaterialBase : LibraryComponent
    {
        [SimulationSetting]
        public double Conductivity { get; set; }

        [SimulationSetting]
        public double Cost { get; set; }

        [SimulationSetting]
        public double Density { get; set; }

        [SimulationSetting(DisplayName = "Embodied Carbon")]
        public double EmbodiedCarbon { get; set; }

        [SimulationSetting(DisplayName = "Embodied Energy")]
        public double EmbodiedEnergy { get; set; }

        [SimulationSetting(DisplayName = "Substitution Rate Pattern")]
        public double[] SubstitutionRatePattern { get; set; }

        [SimulationSetting(DisplayName = "Substitution Timestep")]
        public double SubstitutionTimestep { get; set; }

        [SimulationSetting(DisplayName = "Transportation Carbon")]
        public double TransportCarbon { get; set; }

        [SimulationSetting(DisplayName = "Transportation Distance")]
        public double TransportDistance { get; set; }

        [SimulationSetting(DisplayName = "Transportation Energy")]
        public double TransportEnergy { get; set; }

        protected void CopyBasePropertiesFrom(MaterialBase source)
        {
            Conductivity = source.Conductivity;
            Cost = source.Cost;
            Density = source.Density;
            EmbodiedCarbon = source.EmbodiedCarbon;
            EmbodiedEnergy = source.EmbodiedEnergy;
            SubstitutionTimestep = source.SubstitutionTimestep;
            TransportCarbon = source.TransportCarbon;
            TransportDistance = source.TransportDistance;
            TransportEnergy = source.TransportEnergy;
            CopyBasePropertiesFrom((LibraryComponent)source);
        }
    }
}
