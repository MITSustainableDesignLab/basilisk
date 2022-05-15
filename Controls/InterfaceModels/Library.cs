using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using AutoMapper;

using Basilisk.Controls;
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
                .CreateMap<Core.MaterialBase, MaterialBase>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>();
            Mapper
                .CreateMap<Core.OpaqueMaterial, OpaqueMaterial>()
                .IncludeBase<Core.MaterialBase, MaterialBase>();
            Mapper
                .CreateMap<Core.WindowMaterialBase, WindowMaterialBase>()
                .IncludeBase<Core.MaterialBase, MaterialBase>();
            Mapper
                .CreateMap<Core.GlazingMaterial, GlazingMaterial>()
                .IncludeBase<Core.WindowMaterialBase, WindowMaterialBase>();
            Mapper
                .CreateMap<Core.GasMaterial, GasMaterial>()
                .IncludeBase<Core.WindowMaterialBase, WindowMaterialBase>();
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
                .ForMember(dest => dest.MassRatios, opt => opt.Ignore())
                .ForMember(dest => dest.AdvancedModel, opt => opt.Ignore());
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
                .CreateMap<MaterialBase, Core.MaterialBase>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>();
            Mapper
                .CreateMap<OpaqueMaterial, Core.OpaqueMaterial>()
                .IncludeBase<MaterialBase, Core.MaterialBase>();
            Mapper
                .CreateMap<WindowMaterialBase, Core.WindowMaterialBase>()
                .IncludeBase<MaterialBase, Core.MaterialBase>()
                .ConstructUsing((WindowMaterialBase src) => src is GlazingMaterial ? (Core.WindowMaterialBase)new Core.GlazingMaterial() : new Core.GasMaterial());
            Mapper
                .CreateMap<GlazingMaterial, Core.GlazingMaterial>()
                .IncludeBase<WindowMaterialBase, Core.WindowMaterialBase>();
            Mapper
                .CreateMap<GasMaterial, Core.GasMaterial>()
                .IncludeBase<WindowMaterialBase, Core.WindowMaterialBase>();
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
                .CreateMap<MaterialLayer, Core.MaterialLayer<Core.OpaqueMaterial>>();
            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<Core.WindowMaterialBase>>();
            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<Core.GlazingMaterial>>();
            Mapper
                .CreateMap<MaterialLayer, Core.MaterialLayer<Core.GasMaterial>>();
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
                .CreateMap<
                    AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeBeam>,
                    Core.AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeBeam>>();
            Mapper
                .CreateMap<
                    AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeColumn>,
                    Core.AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeColumn>>();
            Mapper
                .CreateMap<
                    AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFloor>,
                    Core.AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFloor>>();
            Mapper
                .CreateMap<
                    AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFoundation>,
                    Core.AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeFoundation>>();
            Mapper
                .CreateMap<
                    AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeLateralSystem>, 
                    Core.AdvancedStructuralModeling.ConstructionSystem<Core.AdvancedStructuralModeling.ConstructionSystemTypeLateralSystem>>();
            Mapper
                .CreateMap<AdvancedStructuralModeling.ConstructionSystemSettings, Core.AdvancedStructuralModeling.ConstructionSystemSettings>();
            Mapper
                .CreateMap<AdvancedStructuralModeling.ColumnWallSpacingSettings, Core.AdvancedStructuralModeling.ColumnWallSpacingSettings>();
            Mapper
                .CreateMap<AdvancedStructuralModeling.AdvancedStructuralModel, Core.AdvancedStructuralModeling.AdvancedStructuralModel>();
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

        public IEnumerable<LibraryComponent> AllComponents => new ComponentCoordinator(this).AllComponents;

        public ICollection<LibraryComponent> OpaqueMaterials { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> GlazingMaterials { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> GasMaterials { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> OpaqueConstructions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> WindowConstructions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> StructureDefinitions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> DaySchedules { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> WeekSchedules { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> YearSchedules { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneConditionings { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneConstructions { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneHotWaters { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneLoads { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> ZoneVentilations { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> Zones { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> WindowSettings { get; set; } = new List<LibraryComponent>();
        public ICollection<LibraryComponent> BuildingTemplates { get; set; } = new List<LibraryComponent>();

        public static Library Create(Core.Library sourceLib)
        {
            System.Diagnostics.Debug.Assert(!sourceLib.OrphanedComponents().Any());
            var opaqueMats = Mapper.Map<ICollection<OpaqueMaterial>>(sourceLib.OpaqueMaterials).Cast<LibraryComponent>().ToList();
            var glazingMats = Mapper.Map<ICollection<GlazingMaterial>>(sourceLib.GlazingMaterials).Cast<LibraryComponent>().ToList();
            var gasMats = Mapper.Map<ICollection<GasMaterial>>(sourceLib.GasMaterials).Cast<LibraryComponent>().ToList();

            var opaqueMatDict = opaqueMats.ToDictionary(mat => mat.Name);
            var windowMatDict = glazingMats.Concat(gasMats).ToDictionary(mat => mat.Name);

            var opaqueConstructions =
                sourceLib
                .OpaqueConstructions
                .Select(src => BuildLayeredConstruction<Core.OpaqueConstruction, OpaqueConstruction, Core.OpaqueMaterial>(src, opaqueMatDict))
                .ToDictionary(c => c.Name);
            var windowConstructions =
                sourceLib
                .WindowConstructions
                .Select(src => BuildLayeredConstruction<Core.WindowConstruction, WindowConstruction, Core.WindowMaterialBase>(src, windowMatDict))
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
                    for (var i = 0; i < 7; ++i)
                    {
                        var day = default(DaySchedule);
                        if (coreWeek.Days.Length > i && coreWeek.Days[i]?.Name != null)
                        {
                            days.TryGetValue(coreWeek.Days[i].Name, out day);
                        }
                        mapped.Days[i] = day;
                    }
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
                DaySchedules = days.Values.Cast<LibraryComponent>().ToList(),
                WeekSchedules = weeks.Values.Cast<LibraryComponent>().ToList(),
                YearSchedules = years.Values.Cast<LibraryComponent>().ToList(),
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

        public void Add(IEnumerable<LibraryComponent> components)
        {
            var coordinator = new ComponentCoordinator(this);
            foreach (var c in components)
            {
                coordinator.ComponentsOfTypeMutable(c.GetType()).Add(c);
            }
        }

        public void Overwrite(IEnumerable<LibraryComponent> newComponents)
        {
            var coord = new ComponentCoordinator(this);
            foreach (var c in newComponents)
            {
                coord.Get(c.Name, c.GetType()).OverwriteWith(c, coord);
            }
        }

        public Core.Library ToCoreLibrary()
        {
            var newLib = new Core.Library()
            {
                OpaqueMaterials = Mapper.Map<IEnumerable<Core.OpaqueMaterial>>(OpaqueMaterials.Cast<OpaqueMaterial>()).ToList(),
                GlazingMaterials = Mapper.Map<IEnumerable<Core.GlazingMaterial>>(GlazingMaterials.Cast<GlazingMaterial>()).ToList(),
                GasMaterials = Mapper.Map<IEnumerable<Core.GasMaterial>>(GasMaterials.Cast<GasMaterial>()).ToList(),
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
                    .Select(layer => new Core.MaterialLayer<Core.OpaqueMaterial>()
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
                    .Select(layer => new Core.MaterialLayer<Core.WindowMaterialBase>()
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

                if (c.AdvancedModel.ConstructionSystems.Floors.Material?.Name is string materialName)
                {
                    c.AdvancedModel.ConstructionSystems.Floors.Material = knownOpaqueMaterials[materialName];
                }
            }

            newLib.DaySchedules = Mapper.Map<IEnumerable<Core.DaySchedule>>(DaySchedules.Cast<DaySchedule>()).ToList();
            var dayDict = newLib.DaySchedules.ToDictionary(s => s.Name);
            newLib.WeekSchedules =
                WeekSchedules
                .Cast<WeekSchedule>()
                .Select(week =>
                {
                    var mapped = Mapper.Map<Core.WeekSchedule>(week);
                    if (week.Days == null)
                    {
                        throw new InvalidOperationException("A week with a null days collection cannot be saved.");
                    }
                    mapped.Days =
                        week
                        .Days
                        .Select(day => day != null ? dayDict[day.Name] : null)
                        .ToArray();
                    return mapped;
                })
                .ToList();
            var weekDict = newLib.WeekSchedules.ToDictionary(s => s.Name);
            newLib.YearSchedules =
                YearSchedules
                .Cast<YearSchedule>()
                .Select(year =>
                {
                    var mapped = Mapper.Map<Core.YearSchedule>(year);
                    if (year.Parts == null)
                    {
                        throw new InvalidOperationException("Internal error: A year with a null parts collection cannot be saved.");
                    }
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

        public IEnumerable<MergeCollision> WouldCollide(IEnumerable<LibraryComponent> newComponents) =>
            newComponents
            .Join(
                AllComponents,
                newC => newC.Name,
                oldC => oldC.Name,
                (newC, oldC) => new MergeCollision(oldC, newC))
            .Where(x =>
                x.OriginalComponent.GetType() == x.NewComponent.GetType() ||
                ComponentNamespaceAttribute
                    .NamespaceTypes(x.OriginalComponent.GetType())
                    .Any(n => n.IsAssignableFrom(x.NewComponent.GetType())));

        public bool WouldCollide(string name, Type type) =>
            AllComponents
            .Any(c =>
                c.Name == name &&
                (c.GetType() == type ||
                 ComponentNamespaceAttribute
                 .NamespaceTypes(type)
                 .Any(t => t.IsAssignableFrom(c.GetType()))));

        private static DestT BuildLayeredConstruction<SourceT, DestT, MaterialT>(SourceT src, Dictionary<string, LibraryComponent> matDict)
            where SourceT : Core.LayeredConstruction<MaterialT>
            where DestT : LayeredConstruction
            where MaterialT : Core.MaterialBase
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
            var sub = new Library()
            {
                OpaqueMaterials = retrieve(typeof(OpaqueMaterial)),
                GlazingMaterials = retrieve(typeof(GlazingMaterial)),
                GasMaterials = retrieve(typeof(GasMaterial)),
                OpaqueConstructions = retrieve(typeof(OpaqueConstruction)),
                WindowConstructions = retrieve(typeof(WindowConstruction)),
                StructureDefinitions = retrieve(typeof(StructureInformation)),
                DaySchedules = retrieve(typeof(DaySchedule)),
                WeekSchedules = retrieve(typeof(WeekSchedule)),
                YearSchedules = retrieve(typeof(YearSchedule)),
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
#if DEBUG
            System.Diagnostics.Debug.Assert(!core.OrphanedComponents().Any());
#endif
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

            src.AdvancedModel.ColumnWallSpacing ??= new Core.AdvancedStructuralModeling.ColumnWallSpacingSettings();
            dest.AdvancedModel = new AdvancedStructuralModeling.AdvancedStructuralModel
            {
                ColumnWallSpacing = new AdvancedStructuralModeling.ColumnWallSpacingSettings
                {
                    PrimarySpan = src.AdvancedModel.ColumnWallSpacing.PrimarySpan,
                    SecondarySpan = src.AdvancedModel.ColumnWallSpacing.SecondarySpan
                },

                ConstructionSystems = new AdvancedStructuralModeling.ConstructionSystemSettings
                {
                    Beams = CreateSystem("Beams", src.AdvancedModel.ConstructionSystems.Beams),
                    Columns = CreateSystem("Columns", src.AdvancedModel.ConstructionSystems.Columns),
                    Floors = CreateSystem("Floors", src.AdvancedModel.ConstructionSystems.Floors),
                    Foundations = CreateSystem("Foundations", src.AdvancedModel.ConstructionSystems.Foundations),
                    LateralSystem = CreateSystem("Lateral system", src.AdvancedModel.ConstructionSystems.LateralSystem)
                }
            };
            return dest;

            AdvancedStructuralModeling.ConstructionSystem<T> CreateSystem<T>(string name, Core.AdvancedStructuralModeling.ConstructionSystem<T> source)
                where T : Enum =>
                new(name)
                {
                    Material =
                        source.Material is Core.OpaqueMaterial srcM &&
                        matDict.TryGetValue(srcM.Name, out var dstM)
                            ? dstM
                            : null,
                    ConstructionSystemType = source.ConstructionSystemType
                };
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
