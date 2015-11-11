using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneDefinition))]
    public class ZoneDefinition : LibraryComponent
    {
        [SimulationSetting]
        public ZoneConstructions Constructions { get; set; }

        [SimulationSetting]
        public ZoneLoads Loads { get; set; }

        [SimulationSetting]
        public ZoneConditioning Conditioning { get; set; }

        [SimulationSetting]
        public ZoneVentilation Ventilation { get; set; }

        [SimulationSetting(DisplayName = "Domestic hot water")]
        public ZoneHotWater DomesticHotWater { get; set; }

        [SimulationSetting(DisplayName = "Daylight mesh resolution")]
        public double DaylightMeshResolution { get; set; } = 1;

        [SimulationSetting(DisplayName = "Daylight workplane height (m)")]
        public double DaylightWorkplaneHeight { get; set; } = 0.8;

        [SimulationSetting(DisplayName = "Internal mass construction")]
        public OpaqueConstruction InternalMassConstruction { get; set; }

        [SimulationSetting(DisplayName = "Internal mass exposed per floor area")]
        public double InternalMassExposedPerFloorArea { get; set; }

        [SimulationSetting(DisplayName = "Cooling CoP")]
        public double CoolingCoeffOfPerf { get; set; }

        [SimulationSetting(DisplayName = "Heating CoP")]
        public double HeatingCoeffOfPerf { get; set; }

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneDefinition()
            {
                Constructions = Constructions,
                Loads = Loads,
                Conditioning = Conditioning,
                Ventilation = Ventilation,
                DomesticHotWater = DomesticHotWater,
                DaylightMeshResolution = DaylightMeshResolution,
                DaylightWorkplaneHeight = DaylightWorkplaneHeight,
                InternalMassConstruction = InternalMassConstruction,
                InternalMassExposedPerFloorArea = InternalMassExposedPerFloorArea,
                CoolingCoeffOfPerf = CoolingCoeffOfPerf,
                HeatingCoeffOfPerf = HeatingCoeffOfPerf
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
