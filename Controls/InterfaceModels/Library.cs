using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    public class Library
    {
        static Library()
        {
            Mapper
                .CreateMap<Core.LibraryComponent, LibraryComponent>();
            Mapper
                .CreateMap<ArchsimLib.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Category, opt => opt.Ignore());
            Mapper
                .CreateMap<ArchsimLib.BaseMaterial, MaterialBase>()
                .IncludeBase<ArchsimLib.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Conductivity, opt => opt.Ignore())
                .ForMember(dest => dest.Density, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type));
            Mapper
                .CreateMap<ArchsimLib.OpaqueMaterial, OpaqueMaterial>()
                .IncludeBase<ArchsimLib.BaseMaterial, MaterialBase>();
            Mapper
                .CreateMap<ArchsimLib.WindowMaterialBase, WindowMaterialBase>()
                .IncludeBase<ArchsimLib.BaseMaterial, MaterialBase>();
            Mapper
                .CreateMap<ArchsimLib.GlazingMaterial, GlazingMaterial>()
                .IncludeBase<ArchsimLib.WindowMaterialBase, WindowMaterialBase>();
            Mapper
                .CreateMap<ArchsimLib.GasMaterial, GasMaterial>()
                .IncludeBase<ArchsimLib.WindowMaterialBase, WindowMaterialBase>();
            Mapper
                .CreateMap<Core.ConstructionBase, ConstructionBase>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>();
            Mapper
                .CreateMap<Core.OpaqueConstruction, OpaqueConstruction>()
                .IncludeBase<Core.ConstructionBase, ConstructionBase>()
                .ForMember(dest => dest.Layers, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.WindowConstruction, WindowConstruction>()
                .IncludeBase<Core.ConstructionBase, ConstructionBase>()
                .ForMember(dest => dest.Layers, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.StructureInformation, StructureInformation>()
                .IncludeBase<Core.ConstructionBase, ConstructionBase>()
                .ForMember(dest => dest.MassRatios, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.DaySchedule, DaySchedule>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.WeekSchedule, WeekSchedule>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Days, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.YearSchedulePart, YearSchedulePart>()
                .ForMember(dest => dest.Schedule, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.YearSchedule, YearSchedule>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Parts, opt => opt.Ignore());
            Mapper.CreateMap<Core.ZoneConstructions, ZoneConstructions>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Facade, opt => opt.Ignore())
                .ForMember(dest => dest.Ground, opt => opt.Ignore())
                .ForMember(dest => dest.Partition, opt => opt.Ignore())
                .ForMember(dest => dest.Roof, opt => opt.Ignore())
                .ForMember(dest => dest.Slab, opt => opt.Ignore());
            Mapper.CreateMap<Core.ZoneLoads, ZoneLoads>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.OccupancySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.EquipmentAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.LightsAvailabilitySchedule, opt => opt.Ignore());
            Mapper.CreateMap<Core.ZoneConditioning, ZoneConditioning>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.HeatingSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.CoolingSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.MechVentSchedule, opt => opt.Ignore());
            Mapper.CreateMap<Core.ZoneVentilation, ZoneVentilation>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.NatVentSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduledVentilationSchedule, opt => opt.Ignore());
            Mapper.CreateMap<Core.DomesticHotWaterSettings, ZoneHotWater>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.WaterSchedule, opt => opt.Ignore());
            Mapper.CreateMap<Core.ZoneDefinition, ZoneDefinition>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Constructions, opt => opt.Ignore())
                .ForMember(dest => dest.Loads, opt => opt.Ignore())
                .ForMember(dest => dest.Conditioning, opt => opt.Ignore())
                .ForMember(dest => dest.Ventilation, opt => opt.Ignore())
                .ForMember(dest => dest.DomesticHotWater, opt => opt.Ignore())
                .ForMember(dest => dest.InternalMassConstruction, opt => opt.Ignore());
            Mapper.CreateMap<Core.WindowSettings, WindowSettings>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Construction, opt => opt.Ignore())
                .ForMember(dest => dest.ShadingSystemAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneMixingAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.AfnWindowAvailability, opt => opt.Ignore());
            Mapper.CreateMap<Core.BuildingTemplate, BuildingTemplate>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>()
                .ForMember(dest => dest.Core, opt => opt.Ignore())
                .ForMember(dest => dest.Perimeter, opt => opt.Ignore())
                .ForMember(dest => dest.Structure, opt => opt.Ignore())
                .ForMember(dest => dest.Windows, opt => opt.Ignore());

            Mapper
                .CreateMap<LibraryComponent, Core.LibraryComponent>();
            Mapper
                .CreateMap<LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comments));
            Mapper
                .CreateMap<MaterialBase, ArchsimLib.BaseMaterial>()
                .IncludeBase<LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.EmbodiedCarbonStdDev, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedEnergyStdDev, opt => opt.Ignore())
                .ForMember(dest => dest.Life, opt => opt.Ignore());
            Mapper
                .CreateMap<OpaqueMaterial, ArchsimLib.OpaqueMaterial>()
                .IncludeBase<MaterialBase, ArchsimLib.BaseMaterial>()
                .ForMember(dest => dest.PhaseChange, opt => opt.Ignore())
                .ForMember(dest => dest.PhaseChangeProperties, opt => opt.Ignore())
                .ForMember(dest => dest.VariableConductivity, opt => opt.Ignore())
                .ForMember(dest => dest.VariableConductivityProperties, opt => opt.Ignore());
            Mapper
                .CreateMap<WindowMaterialBase, ArchsimLib.WindowMaterialBase>()
                .IncludeBase<MaterialBase, ArchsimLib.BaseMaterial>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Category))
                .ConstructUsing((WindowMaterialBase src) => src is GlazingMaterial ? (ArchsimLib.WindowMaterialBase)new ArchsimLib.GlazingMaterial() : new ArchsimLib.GasMaterial());
            Mapper
                .CreateMap<GlazingMaterial, ArchsimLib.GlazingMaterial>()
                .IncludeBase<WindowMaterialBase, ArchsimLib.WindowMaterialBase>()
                .ForMember(dest => dest.Optical, opt => opt.Ignore())
                .ForMember(dest => dest.OpticalData, opt => opt.Ignore());
            Mapper
                .CreateMap<GasMaterial, ArchsimLib.GasMaterial>()
                .IncludeBase<WindowMaterialBase, ArchsimLib.WindowMaterialBase>()
                .ForMember(dest => dest.GasType, opt => opt.MapFrom(src => src.Name));
            Mapper
                .CreateMap<DaySchedule, Core.DaySchedule>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
            Mapper
                .CreateMap<WeekSchedule, Core.WeekSchedule>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Days, opt => opt.Ignore());
            Mapper
                .CreateMap<YearSchedulePart, Core.YearSchedulePart>()
                .ForMember(dest => dest.Schedule, opt => opt.Ignore());
            Mapper
                .CreateMap<YearSchedule, Core.YearSchedule>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Parts, opt => opt.Ignore());

            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<ArchsimLib.OpaqueMaterial>>();
            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<ArchsimLib.WindowMaterialBase>>();
            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<ArchsimLib.GlazingMaterial>>();
            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<ArchsimLib.GasMaterial>>();
            Mapper
                .CreateMap<MassRatios, Core.MassRatios>();

            Mapper
                .CreateMap<ConstructionBase, Core.ConstructionBase>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>();
            Mapper
                .CreateMap<OpaqueConstruction, Core.OpaqueConstruction>()
                .IncludeBase<ConstructionBase, Core.ConstructionBase>();
            Mapper
                .CreateMap<WindowConstruction, Core.WindowConstruction>()
                .IncludeBase<ConstructionBase, Core.ConstructionBase>();
            Mapper
                .CreateMap<StructureInformation, Core.StructureInformation>()
                .IncludeBase<ConstructionBase, Core.ConstructionBase>();

            Mapper.CreateMap<ZoneConstructions, Core.ZoneConstructions>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Facade, opt => opt.Ignore())
                .ForMember(dest => dest.Ground, opt => opt.Ignore())
                .ForMember(dest => dest.Partition, opt => opt.Ignore())
                .ForMember(dest => dest.Roof, opt => opt.Ignore())
                .ForMember(dest => dest.Slab, opt => opt.Ignore());
            Mapper.CreateMap<ZoneLoads, Core.ZoneLoads>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.OccupancySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.EquipmentAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.LightsAvailabilitySchedule, opt => opt.Ignore());
            Mapper.CreateMap<ZoneConditioning, Core.ZoneConditioning>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.HeatingSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.CoolingSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.MechVentSchedule, opt => opt.Ignore());
            Mapper.CreateMap<ZoneVentilation, Core.ZoneVentilation>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.NatVentSchedule, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduledVentilationSchedule, opt => opt.Ignore());
            Mapper.CreateMap<ZoneHotWater, Core.DomesticHotWaterSettings>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.WaterSchedule, opt => opt.Ignore());
            Mapper.CreateMap<ZoneDefinition, Core.ZoneDefinition>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Constructions, opt => opt.Ignore())
                .ForMember(dest => dest.Loads, opt => opt.Ignore())
                .ForMember(dest => dest.Conditioning, opt => opt.Ignore())
                .ForMember(dest => dest.Ventilation, opt => opt.Ignore())
                .ForMember(dest => dest.DomesticHotWater, opt => opt.Ignore())
                .ForMember(dest => dest.InternalMassConstruction, opt => opt.Ignore());
            Mapper.CreateMap<WindowSettings, Core.WindowSettings>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Construction, opt => opt.Ignore())
                .ForMember(dest => dest.ShadingSystemAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneMixingAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.AfnWindowAvailability, opt => opt.Ignore());
            Mapper.CreateMap<BuildingTemplate, Core.BuildingTemplate>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Core, opt => opt.Ignore())
                .ForMember(dest => dest.Perimeter, opt => opt.Ignore())
                .ForMember(dest => dest.Structure, opt => opt.Ignore())
                .ForMember(dest => dest.Windows, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        public IEnumerable<LibraryComponent> AllComponents =>
            GasMaterials
            .Concat(GlazingMaterials)
            .Concat(OpaqueMaterials)
            .Concat(OpaqueConstructions)
            .Concat(WindowConstructions)
            .Concat(StructureDefinitions)
            .Concat(Schedules)
            .Concat(ZoneConstructions)
            .Concat(ZoneLoads)
            .Concat(ZoneConditionings)
            .Concat(ZoneVentilations)
            .Concat(ZoneHotWaters)
            .Concat(Zones)
            .Concat(WindowSettings)
            .Concat(BuildingTemplates);

        public ICollection<LibraryComponent> GasMaterials { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> GlazingMaterials { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> OpaqueMaterials { get; set; } = new List<LibraryComponent>();

        public ICollection<LibraryComponent> OpaqueConstructions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> WindowConstructions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> StructureDefinitions { get; set; } = new List<LibraryComponent>();

        public ICollection<LibraryComponent> Schedules { get; set; } = new List<LibraryComponent>();

        public ICollection<LibraryComponent> ZoneConstructions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneLoads { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneConditionings { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneVentilations { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneHotWaters { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> Zones { get; set; } = new List<LibraryComponent>();

        public ICollection<LibraryComponent> WindowSettings { get; set; } = new List<LibraryComponent>();

        public ICollection<LibraryComponent> BuildingTemplates { get; set; } = new List<LibraryComponent>();

        public static Library Create(Core.Library sourceLib)
        {
            var opaqueMats = Mapper.Map<ICollection<OpaqueMaterial>>(sourceLib.OpaqueMaterials).Cast<LibraryComponent>().ToList();
            var glazingMats = Mapper.Map<ICollection<GlazingMaterial>>(sourceLib.GlazingMaterials).Cast<LibraryComponent>().ToList();
            var gasMats = Mapper.Map<ICollection<GasMaterial>>(sourceLib.GasMaterials).Cast<LibraryComponent>().ToList();

            var opaqueMatDict = opaqueMats.ToDictionary(mat => mat.Name);
            var windowMatDict = glazingMats.Concat(gasMats).ToDictionary(mat => mat.Name);

            var opaqueConstructions =
                sourceLib
                .OpaqueConstructions
                .Select(src => BuildLayeredConstruction<Core.OpaqueConstruction, OpaqueConstruction, ArchsimLib.OpaqueMaterial>(src, opaqueMatDict))
                .ToDictionary(c => c.Name);
            var windowConstructions =
                sourceLib
                .WindowConstructions
                .Select(src => BuildLayeredConstruction<Core.WindowConstruction, WindowConstruction, ArchsimLib.WindowMaterialBase>(src, windowMatDict))
                .ToDictionary(c => c.Name);
            var structureDefinitions =
                sourceLib
                .StructureDefinitions
                .Select(src => BuildStructureDefinition(src, opaqueMatDict))
                .ToDictionary(c => c.Name);

            var days =
                Mapper
                .Map<ICollection<DaySchedule>>(sourceLib.DaySchedules)
                .ToDictionary(day => day.Name);
            var weeks =
                sourceLib
                .WeekSchedules
                .Select(coreWeek =>
                {
                    var mapped = Mapper.Map<WeekSchedule>(coreWeek);
                    var theseDays =
                        coreWeek
                        .Days
                        .Select(coreDay =>
                        {
                            var day = default(DaySchedule);
                            days.TryGetValue(coreDay.Name, out day);
                            return day;
                        });
                    mapped.Days = new ObservableCollection<DaySchedule>(theseDays);
                    return mapped;
                })
                .ToDictionary(week => week.Name);
            var years =
                sourceLib
                .YearSchedules
                .Select(coreYear =>
                {
                    var mapped = Mapper.Map<YearSchedule>(coreYear);
                    var theseParts =
                        coreYear
                        .Parts
                        .Select(corePart =>
                        {
                            var week = default(WeekSchedule);
                            if (!weeks.TryGetValue(corePart.Schedule.Name, out week))
                            {
                                return null;
                            }
                            var part = Mapper.Map<YearSchedulePart>(corePart);
                            part.Schedule = week;
                            return part;
                        });
                    var year = Mapper.Map<YearSchedule>(coreYear);
                    year.Parts = new ObservableCollection<YearSchedulePart>(theseParts);
                    return year;
                })
                .ToDictionary(s => s.Name);

            var allSchedules =
                days
                .Values
                .Cast<LibraryComponent>()
                .Concat(weeks.Values)
                .Concat(years.Values)
                .ToList();

            var zoneConstructions =
                sourceLib
                .ZoneConstructionSets
                .Select(zc => BuildZoneConstructions(zc, opaqueConstructions))
                .ToDictionary(zc => zc.Name);
            var zoneLoads =
                sourceLib
                .ZoneLoads
                .Select(zl => BuildZoneLoads(zl, years))
                .ToDictionary(zl => zl.Name);
            var zoneConditionings =
                sourceLib
                .ZoneConditionings
                .Select(zc => BuildZoneConditionings(zc, years))
                .ToDictionary(zc => zc.Name);
            var zoneVentilations =
                sourceLib
                .VentilationSettings
                .Select(v => BuildVentilation(v, years))
                .ToDictionary(v => v.Name);
            var zoneHotWaters =
                sourceLib
                .DomesticHotWaterSettings
                .Select(hw => BuildHotWater(hw, years))
                .ToDictionary(hw => hw.Name);
            var windowSettings =
                sourceLib
                .WindowSettings
                .Select(w => BuildWindowSettings(w, windowConstructions, years))
                .ToDictionary(w => w.Name);
            var zones =
                sourceLib
                .Zones
                .Select(z =>
                {
                    var res = Mapper.Map<ZoneDefinition>(z);
                    res.Constructions = zoneConstructions.GetValueOrDefault(z.Constructions?.Name);
                    res.Loads = zoneLoads.GetValueOrDefault(z.Loads?.Name);
                    res.Conditioning = zoneConditionings.GetValueOrDefault(z.Conditioning?.Name);
                    res.Ventilation = zoneVentilations.GetValueOrDefault(z.Ventilation?.Name);
                    res.DomesticHotWater = zoneHotWaters.GetValueOrDefault(z.DomesticHotWater?.Name);
                    res.InternalMassConstruction = opaqueConstructions.GetValueOrDefault(z.InternalMassConstruction?.Name);
                    return res;
                })
                .ToDictionary(z => z.Name);
            var templates =
                sourceLib
                .BuildingTemplates
                .Select(t =>
                {
                    var res = Mapper.Map<BuildingTemplate>(t);
                    res.Core = zones.GetValueOrDefault(t.Core?.Name);
                    res.Perimeter = zones.GetValueOrDefault(t.Perimeter?.Name);
                    res.Structure = structureDefinitions.GetValueOrDefault(t.Structure?.Name);
                    res.Windows = windowSettings.GetValueOrDefault(t.Windows?.Name);
                    return res;
                });

#if DEBUG
            foreach (var z in zones.Values.Where(z => z.InternalMassConstruction != null))
            {
                System.Diagnostics.Debug.Assert(opaqueConstructions.ContainsValue(z.InternalMassConstruction));
            }
#endif

            return new Library()
            {
                OpaqueMaterials = opaqueMats,
                GlazingMaterials = glazingMats,
                GasMaterials = gasMats,
                OpaqueConstructions = opaqueConstructions.Values.Cast<LibraryComponent>().ToList(),
                WindowConstructions = windowConstructions.Values.Cast<LibraryComponent>().ToList(),
                StructureDefinitions = structureDefinitions.Values.Cast<LibraryComponent>().ToList(),
                Schedules = allSchedules,
                ZoneConstructions = zoneConstructions.Values.Cast<LibraryComponent>().ToList(),
                ZoneLoads = zoneLoads.Values.Cast<LibraryComponent>().ToList(),
                ZoneConditionings = zoneConditionings.Values.Cast<LibraryComponent>().ToList(),
                ZoneVentilations = zoneVentilations.Values.Cast<LibraryComponent>().ToList(),
                ZoneHotWaters = zoneHotWaters.Values.Cast<LibraryComponent>().ToList(),
                Zones = zones.Values.Cast<LibraryComponent>().ToList(),
                WindowSettings = windowSettings.Values.Cast<LibraryComponent>().ToList(),
                BuildingTemplates = templates.Cast<LibraryComponent>().ToList()
            };
        }

        public void Import(Library other)
        {
            if (WouldCollide(other).Any())
            {
                throw new InvalidOperationException("The library could not be imported because it would cause at least one name collision.");
            }
            OpaqueMaterials = OpaqueMaterials.Concat(other.OpaqueMaterials).ToList();
            GlazingMaterials = GlazingMaterials.Concat(other.GlazingMaterials).ToList();
            GasMaterials = GasMaterials.Concat(other.GasMaterials).ToList();
            OpaqueConstructions = OpaqueConstructions.Concat(other.OpaqueConstructions).ToList();
            WindowConstructions = WindowConstructions.Concat(other.WindowConstructions).ToList();
            StructureDefinitions = StructureDefinitions.Concat(other.StructureDefinitions).ToList();
            Schedules = Schedules.Concat(other.Schedules).ToList();
            ZoneConstructions = ZoneConstructions.Concat(other.ZoneConstructions).ToList();
            ZoneLoads = ZoneLoads.Concat(other.ZoneLoads).ToList();
            ZoneConditionings = ZoneConditionings.Concat(other.ZoneConditionings).ToList();
            ZoneVentilations = ZoneVentilations.Concat(other.ZoneVentilations).ToList();
            ZoneHotWaters = ZoneHotWaters.Concat(other.ZoneHotWaters).ToList();
            Zones = Zones.Concat(other.Zones).ToList();
            WindowSettings = WindowSettings.Concat(other.WindowSettings).ToList();
            BuildingTemplates = BuildingTemplates.Concat(other.BuildingTemplates).ToList();
        }

        public Core.Library ToCoreLibrary()
        {
            var newLib = new Core.Library()
            {
                OpaqueMaterials = Mapper.Map<IEnumerable<ArchsimLib.OpaqueMaterial>>(OpaqueMaterials.Cast<OpaqueMaterial>()).ToList(),
                GlazingMaterials = Mapper.Map<IEnumerable<ArchsimLib.GlazingMaterial>>(GlazingMaterials.Cast<GlazingMaterial>()).ToList(),
                GasMaterials = Mapper.Map<IEnumerable<ArchsimLib.GasMaterial>>(GasMaterials.Cast<GasMaterial>()).ToList(),
                OpaqueConstructions = Mapper.Map<IEnumerable<Core.OpaqueConstruction>>(OpaqueConstructions.Cast<OpaqueConstruction>()).ToList(),
                WindowConstructions = Mapper.Map<IEnumerable<Core.WindowConstruction>>(WindowConstructions.Cast<WindowConstruction>()).ToList(),
                StructureDefinitions = Mapper.Map<IEnumerable<Core.StructureInformation>>(StructureDefinitions.Cast<StructureInformation>()).ToList(),
                DaySchedules = new List<Core.DaySchedule>(),
                WeekSchedules = new List<Core.WeekSchedule>(),
                YearSchedules = new List<Core.YearSchedule>()
            };
            // These three loops do two things:
            // 1) Remove "dummy" element at the end of the layer/mass ratios lists caused by DataGrids (maybe there's a better way to avoid this)
            // 2) Re-wire materials to eliminate all the clones created by naive automapping
            var knownOpaqueMaterials = newLib.OpaqueMaterials.ToDictionary(c => c.Name);
            var knownWindowMaterials = newLib.AllWindowMaterials.ToDictionary(c => c.Name);
            foreach (var c in newLib.OpaqueConstructions)
            {
                c.Layers =
                    c
                    .Layers
                    .Where(layer => layer.Material != null)
                    .Select(layer => new Core.MaterialLayer<ArchsimLib.OpaqueMaterial>()
                    {
                        Material = knownOpaqueMaterials[layer.Material.Name],
                        Thickness = layer.Thickness
                    })
                    .ToList();
            }
            foreach (var c in newLib.WindowConstructions)
            {
                c.Layers =
                    c
                    .Layers
                    .Where(layer => layer.Material != null)
                    .Select(layer => new Core.MaterialLayer<ArchsimLib.WindowMaterialBase>()
                    {
                        Material = knownWindowMaterials[layer.Material.Name],
                        Thickness = layer.Thickness
                    })
                    .ToList();
            }
            foreach (var c in newLib.StructureDefinitions)
            {
                c.MassRatios =
                    c
                    .MassRatios
                    .Where(ratios => ratios.Material != null)
                    .Select(ratios => new Core.MassRatios()
                    {
                        Material = knownOpaqueMaterials[ratios.Material.Name],
                        NormalRatio = ratios.NormalRatio,
                        HighLoadRatio = ratios.HighLoadRatio
                    })
                    .ToList();
            }

            var schedules = Schedules.GroupBy(s => s.GetType()).ToDictionary(g => g.Key);
            if (schedules.ContainsKey(typeof(DaySchedule)))
            {
                newLib.DaySchedules = Mapper.Map<IEnumerable<Core.DaySchedule>>(schedules[typeof(DaySchedule)].Cast<DaySchedule>()).ToList();
            }
            if (schedules.ContainsKey(typeof(WeekSchedule)))
            {
                var dayDict = newLib.DaySchedules.ToDictionary(day => day.Name);
                newLib.WeekSchedules =
                    schedules[typeof(WeekSchedule)]
                    .Cast<WeekSchedule>()
                    .Select(week =>
                    {
                        var mapped = Mapper.Map<Core.WeekSchedule>(week);
                        mapped.Days =
                            week
                            .Days
                            .Select(day => dayDict[day.Name])
                            .ToArray();
                        return mapped;
                    })
                    .ToList();
            }
            if (schedules.ContainsKey(typeof(YearSchedule)))
            {
                var weekDict = newLib.WeekSchedules.ToDictionary(day => day.Name);
                newLib.YearSchedules =
                    schedules[typeof(YearSchedule)]
                    .Cast<YearSchedule>()
                    .Select(year =>
                    {
                        var mapped = Mapper.Map<Core.YearSchedule>(year);
                        mapped.Parts =
                            year
                            .Parts
                            .Select(part =>
                            {
                                var mappedPart = Mapper.Map<Core.YearSchedulePart>(part);
                                mappedPart.Schedule = weekDict[part.Schedule.Name];
                                return mappedPart;
                            })
                            .ToList();
                        return mapped;
                    })
                    .ToList();
            }
            var knownSchedules = newLib.YearSchedules.ToDictionary(s => s.Name);

            var knownOpaqueConstructions = newLib.OpaqueConstructions.ToDictionary(c => c.Name);
            var knownStructures = newLib.StructureDefinitions.ToDictionary(c => c.Name);
            var knownWindowConstructions = newLib.WindowConstructions.ToDictionary(c => c.Name);
            newLib.ZoneConstructionSets =
                ZoneConstructions
                .Cast<ZoneConstructions>()
                .Select(zc =>
                {
                    var res = Mapper.Map<Core.ZoneConstructions>(zc);
                    res.Facade = knownOpaqueConstructions.GetValueOrDefault(zc.Facade?.Name);
                    res.Ground = knownOpaqueConstructions.GetValueOrDefault(zc.Ground?.Name);
                    res.Partition = knownOpaqueConstructions.GetValueOrDefault(zc.Partition?.Name);
                    res.Roof = knownOpaqueConstructions.GetValueOrDefault(zc.Roof?.Name);
                    res.Slab = knownOpaqueConstructions.GetValueOrDefault(zc.Slab?.Name);
                    return res;
                })
                .ToList();
            newLib.ZoneLoads =
                ZoneLoads
                .Cast<ZoneLoads>()
                .Select(zl =>
                {
                    var res = Mapper.Map<Core.ZoneLoads>(zl);
                    res.OccupancySchedule = knownSchedules.GetValueOrDefault(zl.OccupancySchedule?.Name);
                    res.LightsAvailabilitySchedule = knownSchedules.GetValueOrDefault(zl.LightsAvailabilitySchedule?.Name);
                    res.EquipmentAvailabilitySchedule = knownSchedules.GetValueOrDefault(zl.EquipmentAvailabilitySchedule?.Name);
                    return res;
                })
                .ToList();
            newLib.ZoneConditionings =
                ZoneConditionings
                .Cast<ZoneConditioning>()
                .Select(zc =>
                {
                    var res = Mapper.Map<Core.ZoneConditioning>(zc);
                    res.HeatingSchedule = knownSchedules.GetValueOrDefault(zc.HeatingSchedule?.Name);
                    res.CoolingSchedule = knownSchedules.GetValueOrDefault(zc.CoolingSchedule?.Name);
                    res.MechVentSchedule = knownSchedules.GetValueOrDefault(zc.MechVentSchedule?.Name);
                    return res;
                })
                .ToList();
            newLib.VentilationSettings =
                ZoneVentilations
                .Cast<ZoneVentilation>()
                .Select(v =>
                {
                    var res = Mapper.Map<Core.ZoneVentilation>(v);
                    res.ScheduledVentilationSchedule = knownSchedules.GetValueOrDefault(v.ScheduledVentilationSchedule?.Name);
                    res.NatVentSchedule = knownSchedules.GetValueOrDefault(v.NatVentSchedule?.Name);
                    return res;
                })
                .ToList();
            newLib.DomesticHotWaterSettings =
                ZoneHotWaters
                .Cast<ZoneHotWater>()
                .Select(w =>
                {
                    var res = Mapper.Map<Core.DomesticHotWaterSettings>(w);
                    res.WaterSchedule = knownSchedules.GetValueOrDefault(w.WaterSchedule?.Name);
                    return res;
                })
                .ToList();

            newLib.WindowSettings =
                WindowSettings
                .Cast<WindowSettings>()
                .Select(w =>
                {
                    var res = Mapper.Map<Core.WindowSettings>(w);
                    res.AfnWindowAvailability = knownSchedules.GetValueOrDefault(w.AfnWindowAvailability?.Name);
                    res.ZoneMixingAvailabilitySchedule = knownSchedules.GetValueOrDefault(w.ZoneMixingAvailabilitySchedule?.Name);
                    res.ShadingSystemAvailabilitySchedule = knownSchedules.GetValueOrDefault(w.ShadingSystemAvailabilitySchedule?.Name);
                    res.Construction = knownWindowConstructions.GetValueOrDefault(w.Construction?.Name);
                    return res;
                })
                .ToList();

            var knownConstructionSets = newLib.ZoneConstructionSets.ToDictionary(x => x.Name);
            var knownLoads = newLib.ZoneLoads.ToDictionary(x => x.Name);
            var knownConditionings = newLib.ZoneConditionings.ToDictionary(x => x.Name);
            var knownVentilations = newLib.VentilationSettings.ToDictionary(x => x.Name);
            var knownHotWaters = newLib.DomesticHotWaterSettings.ToDictionary(x => x.Name);
            newLib.Zones =
                Zones
                .Cast<ZoneDefinition>()
                .Select(z =>
                {
                    var res = Mapper.Map<Core.ZoneDefinition>(z);
                    res.Constructions = knownConstructionSets.GetValueOrDefault(z.Constructions?.Name);
                    res.Loads = knownLoads.GetValueOrDefault(z.Loads?.Name);
                    res.Conditioning = knownConditionings.GetValueOrDefault(z.Conditioning?.Name);
                    res.Ventilation = knownVentilations.GetValueOrDefault(z.Ventilation?.Name);
                    res.DomesticHotWater = knownHotWaters.GetValueOrDefault(z.DomesticHotWater?.Name);
                    res.InternalMassConstruction = knownOpaqueConstructions.GetValueOrDefault(z.InternalMassConstruction?.Name);
                    return res;
                })
                .ToList();

            var knownZones = newLib.Zones.ToDictionary(z => z.Name);
            var knownWindowSettings = newLib.WindowSettings.ToDictionary(w => w.Name);
            newLib.BuildingTemplates =
                BuildingTemplates
                .Cast<BuildingTemplate>()
                .Select(t =>
                {
                    var res = Mapper.Map<Core.BuildingTemplate>(t);
                    res.Core = knownZones.GetValueOrDefault(t.Core?.Name);
                    res.Perimeter = knownZones.GetValueOrDefault(t.Perimeter?.Name);
                    res.Structure = knownStructures.GetValueOrDefault(t.Structure?.Name);
                    res.Windows = knownWindowSettings.GetValueOrDefault(t.Windows?.Name);
                    return res;
                })
                .ToList();

            return newLib;
        }

        private static DestT BuildLayeredConstruction<SourceT, DestT, MaterialT>(SourceT src, Dictionary<string, LibraryComponent> matDict)
            where SourceT : Core.LayeredConstruction<MaterialT>
            where DestT : LayeredConstruction
            where MaterialT : ArchsimLib.BaseMaterial
        {
            var dest = Mapper.Map<DestT>(src);
            var layers =
                src
                .Layers
                .Select(layer =>
                {
                    var mappedMat = default(LibraryComponent);
                    if (matDict.TryGetValue(layer.Material.Name, out mappedMat))
                    {
                        return new MaterialLayer()
                        {
                            Material = mappedMat,
                            Thickness = layer.Thickness
                        };
                    }
                    else { return null; }
                })
                .Where(layer => layer != null);
            dest.Layers = new ObservableCollection<MaterialLayer>(layers);
            return dest;
        }

        public IEnumerable<LibraryComponent> WouldCollide(Library other) =>
            // TODO: Make sure this performance isn't terrible (it sure looks terrible here)
            other.AllComponents.Where(c => WouldCollide(c.Name, c.GetType()));

        public bool WouldCollide(string name, Type type)
        {
            var nameMatches = AllComponents.Where(c => String.Equals(name, c.Name, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            if (!nameMatches.Any()) { return false; }
            var namespaceTypes = ComponentNamespaceAttribute.NamespaceTypes(type);
            var nameMatchTypes = nameMatches.Select(c => c.GetType());
            return namespaceTypes.Any(n => nameMatchTypes.Any(m => n.IsAssignableFrom(m)));
        }

        internal static Core.Library CreateSublibrary(IEnumerable<LibraryComponent> withComponents)
        {
            var typed =
                withComponents
                .GroupBy(c => c.GetType())
                .ToDictionary(g => g.Key, g => g.ToList());
            Func<Type, ICollection<LibraryComponent>> retrieve = t =>
            {
                List<LibraryComponent> res;
                return typed.TryGetValue(t, out res) ? res : new List<LibraryComponent>();
            };
            var allSchedules =
                retrieve(typeof(DaySchedule))
                .Concat(retrieve(typeof(WeekSchedule)))
                .Concat(retrieve(typeof(YearSchedule)))
                .ToList();
            var sub = new Library()
            {
                OpaqueMaterials = retrieve(typeof(OpaqueMaterial)),
                GlazingMaterials = retrieve(typeof(GlazingMaterial)),
                GasMaterials = retrieve(typeof(GasMaterial)),
                OpaqueConstructions = retrieve(typeof(OpaqueConstruction)),
                WindowConstructions = retrieve(typeof(WindowConstruction)),
                StructureDefinitions = retrieve(typeof(StructureInformation)),
                Schedules = allSchedules,
                ZoneConstructions = retrieve(typeof(ZoneConstructions)),
                ZoneLoads = retrieve(typeof(ZoneLoads)),
                ZoneConditionings = retrieve(typeof(ZoneConditioning)),
                ZoneHotWaters = retrieve(typeof(ZoneHotWater)),
                ZoneVentilations = retrieve(typeof(ZoneVentilation)),
                Zones = retrieve(typeof(ZoneDefinition)),
                WindowSettings = retrieve(typeof(WindowSettings)),
                BuildingTemplates = retrieve(typeof(BuildingTemplate))
            };
            var core = sub.ToCoreLibrary();
            return core;
        }

        private static StructureInformation BuildStructureDefinition(Core.StructureInformation src, Dictionary<string, LibraryComponent> matDict)
        {
            var dest = Mapper.Map<StructureInformation>(src);
            var massRatios =
                src
                .MassRatios
                .Select(ratios =>
                {
                    var mappedMat = default(LibraryComponent);
                    if (matDict.TryGetValue(ratios.Material.Name, out mappedMat))
                    {
                        return new MassRatios()
                        {
                            Material = mappedMat,
                            NormalRatio = ratios.NormalRatio,
                            HighLoadRatio = ratios.HighLoadRatio
                        };
                    }
                    else { return null; }
                })
                .Where(layer => layer != null);
            dest.MassRatios = new ObservableCollection<MassRatios>(massRatios);
            return dest;
        }

        private static WindowSettings BuildWindowSettings(Core.WindowSettings src, Dictionary<string, WindowConstruction> windowsConstructions, Dictionary<string, YearSchedule> years)
        {
            var dest = Mapper.Map<WindowSettings>(src);
            dest.AfnWindowAvailability = years.GetValueOrDefault(src.AfnWindowAvailability?.Name);
            dest.ZoneMixingAvailabilitySchedule = years.GetValueOrDefault(src.ZoneMixingAvailabilitySchedule?.Name);
            dest.ShadingSystemAvailabilitySchedule = years.GetValueOrDefault(src.ShadingSystemAvailabilitySchedule?.Name);
            dest.Construction = windowsConstructions.GetValueOrDefault(src.Construction?.Name);
            return dest;
        }

        private static ZoneConstructions BuildZoneConstructions(Core.ZoneConstructions src, Dictionary<string, OpaqueConstruction> constructions)
        {
            var dest = Mapper.Map<ZoneConstructions>(src);
            dest.Facade = constructions.GetValueOrDefault(src.Facade?.Name);
            dest.Ground = constructions.GetValueOrDefault(src.Ground?.Name);
            dest.Partition = constructions.GetValueOrDefault(src.Partition?.Name);
            dest.Roof = constructions.GetValueOrDefault(src.Roof?.Name);
            dest.Slab = constructions.GetValueOrDefault(src.Slab?.Name);
            return dest;
        }

        private static ZoneLoads BuildZoneLoads(Core.ZoneLoads src, Dictionary<string, YearSchedule> schedules)
        {
            var dest = Mapper.Map<ZoneLoads>(src);
            dest.OccupancySchedule = schedules.GetValueOrDefault(src.OccupancySchedule?.Name);
            dest.LightsAvailabilitySchedule = schedules.GetValueOrDefault(src.LightsAvailabilitySchedule?.Name);
            dest.EquipmentAvailabilitySchedule = schedules.GetValueOrDefault(src.EquipmentAvailabilitySchedule?.Name);
            return dest;
        }

        private static ZoneConditioning BuildZoneConditionings(Core.ZoneConditioning src, Dictionary<string, YearSchedule> schedules)
        {
            var dest = Mapper.Map<ZoneConditioning>(src);
            dest.HeatingSchedule = schedules.GetValueOrDefault(src.HeatingSchedule?.Name);
            dest.CoolingSchedule = schedules.GetValueOrDefault(src.CoolingSchedule?.Name);
            dest.MechVentSchedule = schedules.GetValueOrDefault(src.MechVentSchedule?.Name);
            return dest;
        }
        
        private static ZoneVentilation BuildVentilation(Core.ZoneVentilation src, Dictionary<string, YearSchedule> schedules)
        {
            var dest = Mapper.Map<ZoneVentilation>(src);
            dest.ScheduledVentilationSchedule = schedules.GetValueOrDefault(src.ScheduledVentilationSchedule?.Name);
            dest.NatVentSchedule = schedules.GetValueOrDefault(src.NatVentSchedule?.Name);
            return dest;
        }

        private static ZoneHotWater BuildHotWater(Core.DomesticHotWaterSettings src, Dictionary<string, YearSchedule> schedules)
        {
            var dest = Mapper.Map<ZoneHotWater>(src);
            dest.WaterSchedule = schedules.GetValueOrDefault(src.WaterSchedule?.Name);
            return dest;
        }
    }
}
