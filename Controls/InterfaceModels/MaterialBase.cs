﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    public abstract class MaterialBase : LibraryComponent
    {
        static MaterialBase()
        {
            Mapper
                .CreateMap<Core.MaterialBase, MaterialBase>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>();
        }

        [SimulationSetting]
        public double Conductivity { get; set; }

        [SimulationSetting]
        public double Cost { get; set; }

        [SimulationSetting]
        public double Density { get; set; }

        [SimulationSetting(DisplayName = "Embodied Carbon")]
        public double EmbodiedCarbon { get; set; }

        [SimulationSetting(DisplayName = "Embodied Carbon Standard Deviation")]
        public double EmbodiedCarbonStdDev { get; set; }

        [SimulationSetting(DisplayName = "Embodied Energy")]
        public double EmbodiedEnergy { get; set; }

        [SimulationSetting(DisplayName = "Embodied Energy Standard Deviation")]
        public double EmbodiedEnergyStdDev { get; set; }

        [SimulationSetting(DisplayName = "Substitution Timestep")]
        public double SubstitutionTimestep { get; set; }

        [SimulationSetting(DisplayName = "Transportation Carbon")]
        public double TransportCarbon { get; set; }

        [SimulationSetting(DisplayName = "Transportation Distance")]
        public double TransportDistance { get; set; }

        [SimulationSetting(DisplayName = "Transportation Energy")]
        public double TransportEnergy { get; set; }
    }
}