using System;
using System.Collections.Generic;
using System.Linq;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class ComponentCoordinator
    {
        private readonly Library lib;

        public ComponentCoordinator(Library lib) { this.lib = lib; }

        public IEnumerable<LibraryComponent> ComponentsOfType(Type type)
        {
            if (type == typeof(OpaqueMaterial)) { return lib.OpaqueMaterials; }
            else if (type == typeof(GlazingMaterial)) { return lib.GlazingMaterials; }
            else if (type == typeof(GasMaterial)) { return lib.GasMaterials; }
            else if (type == typeof(OpaqueConstruction)) { return lib.OpaqueConstructions; }
            else if (type == typeof(WindowConstruction)) { return lib.WindowConstructions; }
            else if (type == typeof(StructureInformation)) { return lib.StructureDefinitions; }
            else if (type == typeof(DaySchedule)) { return lib.DaySchedules; }
            else if (type == typeof(WeekSchedule)) { return lib.WeekSchedules; }
            else if (type == typeof(YearSchedule)) { return lib.YearSchedules; }
            else if (type == typeof(ZoneConditioning)) { return lib.ZoneConditionings; }
            else if (type == typeof(ZoneConstructions)) { return lib.ZoneConstructions; }
            else if (type == typeof(ZoneHotWater)) { return lib.ZoneHotWaters; }
            else if (type == typeof(ZoneLoads)) { return lib.ZoneLoads; }
            else if (type == typeof(ZoneVentilation)) { return lib.ZoneVentilations; }
            else if (type == typeof(ZoneDefinition)) { return lib.Zones; }
            else if (type == typeof(BuildingTemplate)) { return lib.BuildingTemplates; }
            else if (type == typeof(WindowSettings)) { return lib.WindowSettings; }
            else { throw new NotSupportedException($"Components of type '{type.Name}' cannot be retrieved from a library."); }
        }
    }
}
