using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public class GlazingMaterial : BaseMaterial
    {
        public double DirtFactor { get; set; }
        public double IREmissivityBack { get; set; }
        public double IREmissivityFront { get; set; }
        public double IRTransmittance { get; set; }
        public string Optical { get; set; }
        public string OpticalData { get; set; }
        public double SolarTransmittance { get; set; }
        public double SolarReflectanceBack { get; set; }
        public double SolarReflectanceFront { get; set; }
        public double VisibleReflectanceBack { get; set; }
        public double VisibleReflectanceFront { get; set; }
        public double VisibleTransmittance { get; set; }
    }
}
