using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ConstructionBase))]
    public abstract class ConstructionBase : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Assembly Carbon (kgCO2/m2)")]
        public double AssemblyCarbon { get; set; }

        [SimulationSetting(DisplayName = "Assembly Energy (MJ/m2)")]
        public double AssemblyEnergy { get; set; }

        [SimulationSetting(DisplayName = "Disassembly Carbon (kgCO2/m2)")]
        public double DisassemblyCarbon { get; set; }

        [SimulationSetting(DisplayName = "Disassembly Energy (MJ/m2)")]
        public double DisassemblyEnergy { get; set; }

        [SimulationSetting(DisplayName = "Cost ($/m3)")]
        public double AssemblyCost { get; set; }

        protected void CopyBasePropertiesFrom(ConstructionBase source)
        {
            AssemblyCarbon = source.AssemblyCarbon;
            AssemblyCost = source.AssemblyCost;
            AssemblyEnergy = source.AssemblyEnergy;
            DisassemblyCarbon = source.DisassemblyCarbon;
            DisassemblyEnergy = source.DisassemblyEnergy;
            CopyBasePropertiesFrom((LibraryComponent)source);
        }
    }
}
