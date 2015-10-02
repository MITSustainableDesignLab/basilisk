using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public abstract class BaseMaterial
    {
        public string Comments { get; set; }
        public double Conductivity { get; set; }
        public double Cost { get; set; }
        public double Density { get; set; }
        public string DataSource { get; set; }
        public double ECStandardDev { get; set; }
        public double EEStandardDev { get; set; }
        public double EmbodiedCarbon { get; set; }
        public double EmbodiedEnergy { get; set; }
        public string Name { get; set; }
        public double[] SubstituionRatePattern { get; set; }
        public double SubstituionTimeStep { get; set; }
        public double TransportCarbon { get; set; }
        public double TransportDist { get; set; }
        public double TransportEnergy { get; set; }
    }
}
