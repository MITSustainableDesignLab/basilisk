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

        public IEnumerable<LibraryComponent> AllComponents =>
            lib.OpaqueMaterials
            .Concat(lib.GlazingMaterials)
            .Concat(lib.GasMaterials)
            .Concat(lib.OpaqueConstructions)
            .Concat(lib.WindowConstructions)
            .Concat(lib.StructureDefinitions)
            .Concat(lib.DaySchedules)
            .Concat(lib.WeekSchedules)
            .Concat(lib.YearSchedules)
            .Concat(lib.ZoneConditionings)
            .Concat(lib.ZoneConstructions)
            .Concat(lib.ZoneHotWaters)
            .Concat(lib.ZoneLoads)
            .Concat(lib.ZoneVentilations)
            .Concat(lib.Zones)
            .Concat(lib.BuildingTemplates)
            .Concat(lib.WindowSettings);

        public IEnumerable<LibraryComponent> ComponentsOfType(Type type) => ComponentsOfTypeMutable(type);

        public T Get<T>(string name) where T : LibraryComponent =>
            // Linear scan until performance gets noticeably bad
            ComponentsOfType(typeof(T)).SingleOrDefault(c => c.Name == name) as T;

        public LibraryComponent Get(string name, Type concreteType) =>
            ComponentsOfType(concreteType).SingleOrDefault(c => c.Name == name);

        public T GetWithSameName<T>(T c) where T : LibraryComponent =>
            Get<T>(c?.Name);

        internal ICollection<LibraryComponent> ComponentsOfTypeMutable(Type type)
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
