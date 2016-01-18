﻿using System.Collections.Generic;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneDefinition))]
    [DisplayName("zone definition")]
    [ComponentNamespace]
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

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    Constructions,
                    Loads,
                    Conditioning,
                    Ventilation,
                    DomesticHotWater,
                    InternalMassConstruction
                }.Where(d => d != null);
                return
                    direct
                    .Concat(direct.SelectMany(d => d.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            Constructions == component ||
            Loads == component ||
            Conditioning == component ||
            Ventilation == component ||
            DomesticHotWater == component ||
            InternalMassConstruction == component;

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
                InternalMassExposedPerFloorArea = InternalMassExposedPerFloorArea
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
