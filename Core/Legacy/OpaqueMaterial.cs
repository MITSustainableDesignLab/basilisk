using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core.Legacy
{
    public class OpaqueMaterial : BaseMaterial
    {
        public string Roughness { get; set; }
        public double SolarAbsorptance { get; set; }
        public double SpecificHeat { get; set; }
        public double ThermalEmittance { get; set; }
        public double VisibleAbsorptance { get; set; }
    }
}
