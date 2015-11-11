using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class ZoneDefinition : LibraryComponent
    {
        [DataMember]
        public double CoolingCoeffOfPerf { get; set; }

        [DataMember]
        public double HeatingCoeffOfPerf { get; set; }

        [DataMember]
        public ZoneConditioning Conditioning { get; set; }

        [DataMember]
        public ZoneConstructions Constructions { get; set; }

        [DataMember, DefaultValue(1)]
        public double DaylightMeshResolution { get; set; } = 1;

        [DataMember, DefaultValue(0.8)]
        public double DaylightWorkplaneHeight { get; set; } = 0.8;

        [DataMember]
        public DomesticHotWaterSettings DomesticHotWater { get; set; }

        [DataMember]
        public OpaqueConstruction InternalMassConstruction { get; set; }

        [DataMember]
        public double InternalMassExposedPerFloorArea { get; set; }

        [DataMember]
        public ZoneLoads Loads { get; set; }

        [DataMember]
        public ZoneVentilation Ventilation { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    Constructions,
                    Conditioning,
                    DomesticHotWater,
                    InternalMassConstruction,
                    Loads,
                    Ventilation
                }.Where(c => c != null);
                return direct.Concat(direct.SelectMany(c => c.ReferencedComponents));
            }
        }
    }
}
