using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Core = Basilisk.Core;

namespace Basilisk.Legacy
{
    public static class Conversion
    {
        static Conversion()
        {
            Mapper
                .CreateMap<Legacy.BaseMaterial, ArchsimLib.BaseMaterial>()
                .ForMember(dest => dest.SubstitutionTimestep, opt => opt.MapFrom(src => src.SubstituionTimeStep))
                .ForMember(dest => dest.SubstitutionRatePattern, opt => opt.MapFrom(src => src.SubstituionRatePattern))
                .ForMember(dest => dest.TransportDistance, opt => opt.MapFrom(src => src.TransportDist))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Life, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedCarbonStdDev, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedEnergyStdDev, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.OpaqueMaterial, ArchsimLib.OpaqueMaterial>()
                .IncludeBase<Legacy.BaseMaterial, ArchsimLib.BaseMaterial>()
                .ForMember(dest => dest.PhaseChange, opt => opt.Ignore())
                .ForMember(dest => dest.PhaseChangeProperties, opt => opt.Ignore())
                .ForMember(dest => dest.VariableConductivity, opt => opt.Ignore())
                .ForMember(dest => dest.VariableConductivityProperties, opt => opt.Ignore())
                .ForMember(dest => dest.MoistureDiffusionResistance, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.GlazingMaterial, ArchsimLib.GlazingMaterial>()
                .IncludeBase<Legacy.BaseMaterial, ArchsimLib.BaseMaterial>();
            Mapper
                .CreateMap<Legacy.GasMaterial, ArchsimLib.GasMaterial>()
                .ForMember(dest => dest.GasType, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cost, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedCarbon, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedEnergy, opt => opt.Ignore())
                .ForMember(dest => dest.SubstitutionRatePattern, opt => opt.Ignore())
                .ForMember(dest => dest.SubstitutionTimestep, opt => opt.Ignore())
                .ForMember(dest => dest.TransportCarbon, opt => opt.Ignore())
                .ForMember(dest => dest.TransportDistance, opt => opt.Ignore())
                .ForMember(dest => dest.TransportEnergy, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Life, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedCarbonStdDev, opt => opt.Ignore())
                .ForMember(dest => dest.EmbodiedEnergyStdDev, opt => opt.Ignore());

            Mapper
                .CreateMap<Legacy.OpaqueConstruction, Core.OpaqueConstruction>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.ResolveUsing((Legacy.OpaqueConstruction src) =>
                {
                    var legacy = src.Type.Replace(" ", String.Empty);
                    var type = default(ArchsimLib.ConstructionTypes);
                    Enum.TryParse(legacy, true, out type);
                    return type;
                }))
                .ForMember(dest => dest.Layers, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.GlazingConstruction, Core.WindowConstruction>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Type, opt => opt.ResolveUsing((Legacy.GlazingConstruction src) =>
                {
                    var legacy = src.Type.Replace(" ", String.Empty);
                    var type = default(ArchsimLib.GlazingConstructionTypes);
                    Enum.TryParse(legacy, true, out type);
                    return type;
                }))
                .ForMember(dest => dest.Layers, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.StructureType, Core.StructureInformation>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.MassRatios, opt => opt.Ignore());

            Mapper
                .CreateMap<Legacy.DaySchedule, Core.DaySchedule>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.WeekSchedule, Core.WeekSchedule>()
                .ForMember(dest => dest.Days, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.YearSchedule, Core.YearSchedule>()
                .ForMember(dest => dest.Parts, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            Mapper
                .CreateMap<Legacy.BuildingTemplate, Core.WindowSettings>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Name, opt => opt.ResolveUsing((BuildingTemplate src) => $"{src.Name} windows"))
                .ForMember(dest => dest.IsShadingSystemOn, opt => opt.MapFrom(src => src.BlindOn))
                .ForMember(dest => dest.ShadingSystemTransmittance, opt => opt.MapFrom(src => src.BlindTrns))
                .ForMember(dest => dest.ShadingSystemSetpoint, opt => opt.MapFrom(src => src.BlindSetWatt))
                .ForMember(dest => dest.Type, opt => opt.ResolveUsing((BuildingTemplate src) =>
                {
                    var res = Core.WindowType.External;
                    Enum.TryParse(src.BlindT.ToString(), true, out res);
                    return res;
                }))
                .ForMember(dest => dest.ShadingSystemAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneMixingAvailabilitySchedule, opt => opt.Ignore())
                .ForMember(dest => dest.Construction, opt => opt.Ignore())
                .ForMember(dest => dest.AfnDischargeC, opt => opt.Ignore())
                .ForMember(dest => dest.AfnTempSetpoint, opt => opt.Ignore())
                .ForMember(dest => dest.AfnWindowAvailability, opt => opt.Ignore())
                .ForMember(dest => dest.IsVirtualPartition, opt => opt.Ignore())
                .ForMember(dest => dest.IsZoneMixingOn, opt => opt.Ignore())
                .ForMember(dest => dest.OperableArea, opt => opt.Ignore())
                .ForMember(dest => dest.ShadingSystemType, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneMixingDeltaTemperature, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneMixingFlowRate, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        public static Core.Library Convert(Library legacy)
        {
            var res = new Core.Library()
            {
                OpaqueMaterials = Mapper.Map<IEnumerable<ArchsimLib.OpaqueMaterial>>(legacy.OpaqueMaterials).ToList(),
                GlazingMaterials = Mapper.Map<IEnumerable<ArchsimLib.GlazingMaterial>>(legacy.GlazingMaterials).ToList(),
                GasMaterials = Mapper.Map<IEnumerable<ArchsimLib.GasMaterial>>(legacy.GasMaterials).ToList(),
                DaySchedules = Mapper.Map<IEnumerable<Core.DaySchedule>>(legacy.DaySchedules).ToList()
            };

            var lookupOpaqueMat = res.LookupArchsim(lib => lib.OpaqueMaterials);
            var lookupWindowMat = res.LookupArchsim(lib => lib.AllWindowMaterials);

            res.OpaqueConstructions =
                legacy
                .OpaqueConstructions
                .Select(c => c.Map<Core.OpaqueConstruction, ArchsimLib.OpaqueMaterial, OpaqueLayer>(lookupOpaqueMat))
                .ToList();
            res.WindowConstructions =
                legacy
                .GlazingConstructions
                .Select(c => c.Map<Core.WindowConstruction, ArchsimLib.WindowMaterialBase, GlazingLayer>(lookupWindowMat))
                .ToList();
            res.StructureDefinitions =
                legacy
                .StructureTypes
                .Select(s => s.Map(lookupOpaqueMat))
                .ToList();
            var lookupOpaqueConstruction = res.LookupCore(lib => lib.OpaqueConstructions);
            var lookupWindowConstruction = res.LookupCore(lib => lib.WindowConstructions);
            var lookupStructure = res.LookupCore(lib => lib.StructureDefinitions);

            var lookupDaySchedule = res.LookupCore(lib => lib.DaySchedules);
            res.WeekSchedules =
                legacy
                .WeekSchedules
                .Select(s => s.Map(lookupDaySchedule))
                .Where(s => s != null)
                .ToList();
            var lookupWeekSchedule = res.LookupCore(lib => lib.WeekSchedules);
            res.YearSchedules =
                legacy
                .YearSchedules
                .Select(s => s.Map(lookupWeekSchedule))
                .Where(s => s != null)
                .ToList();
            var lookupYearSchedule = res.LookupCore(lib => lib.YearSchedules);

            res.WindowSettings =
                legacy
                .BuildingTemplates
                .Select(s => s.Map(lookupYearSchedule, lookupWindowConstruction))
                .Where(s => s != null)
                .ToList();
            var lookupWindowSettings = res.LookupCore(lib => lib.WindowSettings);

            res.BuildingTemplates =
                legacy
                .BuildingTemplates
                .Select(template => template.Map(
                    lookupOpaqueConstruction,
                    lookupWindowSettings,
                    lookupStructure,
                    lookupYearSchedule))
                .ToList();
            // Imported legacy templates have the same zone object for core and perimeter, so we'll just use core
            res.Zones = res.BuildingTemplates.Select(t => t.Core).ToList();
            res.ZoneConditionings = res.Zones.Select(z => z.Conditioning).ToList();
            res.ZoneConstructionSets = res.Zones.Select(z => z.Constructions).ToList();
            res.DomesticHotWaterSettings = res.Zones.Select(z => z.DomesticHotWater).ToList();
            res.ZoneLoads = res.Zones.Select(z => z.Loads).ToList();
            res.VentilationSettings = res.Zones.Select(z => z.Ventilation).ToList();

#if DEBUG
            foreach (var z in res.Zones)
            {
                var massC = z.InternalMassConstruction;
                System.Diagnostics.Debug.Assert(massC == null || res.OpaqueConstructions.Contains(massC));
                System.Diagnostics.Debug.Assert(massC.Layers != null);
            }
#endif

            return res;
        }

        private static Func<string, ComponentT> LookupArchsim<ComponentT>(this Core.Library lib, Func<Core.Library, IEnumerable<ComponentT>> getComponents)
            where ComponentT : ArchsimLib.LibraryComponent
        {
            var dict = getComponents(lib).ToDictionary(c => c.Name);
            return name =>
            {
                var res = default(ComponentT);
                dict.TryGetValue(name, out res);
                return res;
            };
        }

        private static Func<string, ComponentT> LookupCore<ComponentT>(this Core.Library lib, Func<Core.Library, IEnumerable<ComponentT>> getComponents)
            where ComponentT : Core.LibraryComponent
        {
            var dict = getComponents(lib).ToDictionary(c => c.Name);
            return name =>
            {
                var res = default(ComponentT);
                dict.TryGetValue(name, out res);
                return res;
            };
        }

        private static Core.BuildingTemplate Map(
            this BuildingTemplate src,
            Func<string, Core.OpaqueConstruction> getMappedOpaqueConstruction,
            Func<string, Core.WindowSettings> getMappedWindow,
            Func<string, Core.StructureInformation> getMappedStructure,
            Func<string, Core.YearSchedule> getMappedSchedule)
        {
            var zone = new Core.ZoneDefinition()
            {
                Name = src.Name,
                DataSource = src.DataSource,
                Category = src.Type
            };
            zone.Conditioning = new Core.ZoneConditioning()
            {
                Name = $"{src.Name} conditioning",
                Category = src.Type,
                DataSource = src.DataSource,
                CoolingSchedule = getMappedSchedule(src.CoolingSchd),
                CoolingSetpoint = src.CoolingSet,
                CoolingCoeffOfPerf = src.CoolingCoP,
                HeatingSchedule = getMappedSchedule(src.HeatingSchd),
                HeatingSetpoint = src.HeatingSet,
                HeatingCoeffOfPerf = src.HeatingCoP,
                IsCoolingOn = src.CoolingOn,
                IsHeatingOn = src.HeatingOn,
                IsMechVentOn = src.MechVentOn,
                MechVentSchedule = getMappedSchedule(src.MechVentSchd),
                MinFreshAirPerArea = src.MechVentMinFreshAirPerArea,
                MinFreshAirPerPerson = src.MechVentMinFreshAirPerson
            };
            zone.Constructions = new Core.ZoneConstructions()
            {
                Name = $"{src.Name} constructions",
                Category = src.Type,
                DataSource = src.DataSource,
                Facade = getMappedOpaqueConstruction(src.FacadeWl),
                Ground = getMappedOpaqueConstruction(src.GroundFl),
                Partition = getMappedOpaqueConstruction(src.PartitionWl),
                Roof = getMappedOpaqueConstruction(src.RoofFl),
                Slab = getMappedOpaqueConstruction(src.InteriorFl)
            };
            zone.DomesticHotWater = new Core.DomesticHotWaterSettings()
            {
                Name = $"{src.Name} hot water",
                Category = src.Type,
                DataSource = src.DataSource,
                IsOn = src.WaterOn,
                FlowRatePerFloorArea = src.WaterPeakFlow,
                WaterSchedule = getMappedSchedule(src.WaterSchd),
                WaterSupplyTemperature = src.WaterTempSup,
                WaterTemperatureInlet = src.WaterTempIn
            };
            zone.InternalMassConstruction = getMappedOpaqueConstruction(src.MassConst);
            zone.InternalMassExposedPerFloorArea = src.MassRatio;
            zone.Loads = new Core.ZoneLoads()
            {
                Name = $"{src.Name} loads",
                Category = src.Type,
                DataSource = src.DataSource,
                EquipmentAvailabilitySchedule = getMappedSchedule(src.EquipSchd),
                IlluminanceTarget = src.LuxTarget,
                LightingPowerDensity = src.LightDnst,
                LightsAvailabilitySchedule = getMappedSchedule(src.LightSchd),
                OccupancySchedule = getMappedSchedule(src.OccupSchd),
                PeopleDensity = src.OccupDnst
            };
            zone.Ventilation = new Core.ZoneVentilation()
            {
                Name = $"{src.Name} ventilation",
                Category = src.Type,
                DataSource = src.DataSource,
                Infiltration = src.Infiltration,
                IsNatVentOn = src.NatVentOn,
                NatVentMinOutdoorAirTemp = src.NatVMinTout,
                NatVentSchedule = getMappedSchedule(src.NatVentSchd),
                NatVentZoneTempSetpoint = src.NatVMinTin,
                ScheduledVentilationSetpoint = src.NatVMinTin,
                ScheduledVentilationSchedule = getMappedSchedule(src.MechVentSchd)
            };
            return new Core.BuildingTemplate()
            {
                Name = src.Name,
                DataSource = src.DataSource,
                Category = src.Type,
                Comments = src.Comments,
                Core = zone,
                Lifespan = src.LifeSpan,
                PartitionRatio = src.PartRatio,
                Perimeter = zone,
                Structure = getMappedStructure(src.StructureTy),
                Windows = getMappedWindow($"{src.Name} windows")
            };
        }

        private static Core.MassRatios Map(this MaterialComp src, Func<string, ArchsimLib.OpaqueMaterial> getMappedMat) =>
            new Core.MassRatios()
            {
                HighLoadRatio = src.QuantRatioHigh,
                Material = getMappedMat(src.MaterialName),
                NormalRatio = src.QuantRatio
            };

        private static Core.StructureInformation Map(this StructureType src, Func<string, ArchsimLib.OpaqueMaterial> getMappedMat)
        {
            var ratios =
                src
                .Materials
                .Select(m => m.Map(getMappedMat))
                .Where(r => r.Material != null)
                .ToList();
            var res = Mapper.Map<Core.StructureInformation>(src);
            res.MassRatios = ratios;
            return res;
        }

        private static TargetConstructionT Map<TargetConstructionT, TargetMaterialT, SourceLayerT>(this BaseConstruction<SourceLayerT> src, Func<string, TargetMaterialT> getMappedMat)
            where TargetMaterialT : ArchsimLib.BaseMaterial
            where TargetConstructionT : Core.LayeredConstruction<TargetMaterialT>, new()
            where SourceLayerT : BaseLayer
        {
            var res = Mapper.Map<TargetConstructionT>(src);
            res.Layers =
                src
                .Layers
                .Select(srcLayer => new Core.MaterialLayer<TargetMaterialT>()
                {
                    Thickness = srcLayer.Thickness,
                    Material = getMappedMat(srcLayer.MaterialName)
                })
                .Where(layer => layer.Material != null)
                .ToList();
            return res;
        }

        private static Core.WeekSchedule Map(this WeekSchedule src, Func<string, Core.DaySchedule> getMappedDay)
        {
            var days = src.Days.Select(getMappedDay).ToArray();
            if (days.Any(day => day == null)) { return null; }
            var res = Mapper.Map<Core.WeekSchedule>(src);
            res.Days = days;
            return res;
        }

        private static Core.YearSchedule Map(this YearSchedule src, Func<string, Core.WeekSchedule> getMappedWeek)
        {
            if (src.DayFrom.Count != src.DayTill.Count ||
                src.DayFrom.Count != src.MonthFrom.Count ||
                src.DayFrom.Count != src.MonthTill.Count ||
                src.DayFrom.Count != src.WeekScheduleNames.Count)
            {
                return null;
            }
            var newWeeks = src.WeekScheduleNames.Select(getMappedWeek).ToArray();
            if (newWeeks.Any(w => w == null)) { return null; }
            var res = Mapper.Map<Core.YearSchedule>(src);
            res.Parts = new List<Core.YearSchedulePart>();
            for (var i = 0; i < src.DayFrom.Count; ++i)
            {
                res.Parts.Add(new Core.YearSchedulePart()
                {
                    Schedule = newWeeks[i],
                    FromDay = src.DayFrom[i],
                    FromMonth = src.MonthFrom[i],
                    ToDay = src.DayTill[i],
                    ToMonth = src.MonthTill[i]
                });
            }
            return res;
        }

        private static Core.WindowSettings Map(this BuildingTemplate src, Func<string, Core.YearSchedule> getMappedYear, Func<string, Core.WindowConstruction> getMappedWindow)
        {
            var res = Mapper.Map<Core.WindowSettings>(src);
            res.AfnWindowAvailability = getMappedYear(src.BlindSchd);
            res.ShadingSystemAvailabilitySchedule = getMappedYear(src.BlindSchd);
            res.ZoneMixingAvailabilitySchedule = getMappedYear(src.BlindSchd);
            res.Construction = getMappedWindow(src.Glazing);
            return res;
        }
    }
}
