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
                .CreateMap<Legacy.BaseMaterial, Core.MaterialBase>()
                .ForMember(dest => dest.EmbodiedCarbonStdDev, opt => opt.MapFrom(src => src.ECStandardDev))
                .ForMember(dest => dest.EmbodiedEnergyStdDev, opt => opt.MapFrom(src => src.EEStandardDev))
                .ForMember(dest => dest.SubstitutionTimestep, opt => opt.MapFrom(src => src.SubstituionTimeStep))
                .ForMember(dest => dest.SubstitutionRatePattern, opt => opt.MapFrom(src => src.SubstituionRatePattern));
            Mapper
                .CreateMap<Legacy.OpaqueMaterial, Core.OpaqueMaterial>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type));
            Mapper
                .CreateMap<Legacy.GlazingMaterial, Core.GlazingMaterial>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type));
            Mapper
                .CreateMap<Legacy.GasMaterial, Core.GasMaterial>();
            
            Mapper
                .CreateMap<Legacy.OpaqueConstruction, Core.OpaqueConstruction>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Layers, opt => opt.ResolveUsing(ResolveOpaqueMaterialLayers));
            Mapper
                .CreateMap<Legacy.GlazingConstruction, Core.WindowConstruction>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Layers, opt => opt.ResolveUsing(ResolveWindowMaterialLayers));
            Mapper
                .CreateMap<Legacy.StructureType, Core.StructureInformation>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.MassRatios, opt => opt.ResolveUsing(ResolveMassRatios));

            Mapper
                .CreateMap<Legacy.DaySchedule, Core.DaySchedule>();
            Mapper
                .CreateMap<Legacy.WeekSchedule, Core.WeekSchedule>()
                .ForMember(dest => dest.Days, opt => opt.ResolveUsing(ResolveWeekScheduleDays));
            Mapper
                .CreateMap<Legacy.YearSchedule, Core.YearSchedule>()
                .ForMember(dest => dest.Parts, opt => opt.ResolveUsing(ResolveYearScheduleParts));

            Mapper
                .CreateMap<Legacy.BuildingTemplate, Core.ZoneDefinition>()
                .ForMember(dest => dest.Basement, opt => opt.ResolveUsing(OpaqueConstructionResolver(template => template.BasementWl)))
                .ForMember(dest => dest.ExteriorFloor, opt => opt.ResolveUsing(OpaqueConstructionResolver(template => template.ExteriorFl)))
                .ForMember(dest => dest.Facade, opt => opt.ResolveUsing(OpaqueConstructionResolver(template => template.FacadeWl)))
                .ForMember(dest => dest.Ground, opt => opt.ResolveUsing(OpaqueConstructionResolver(template => template.GroundFl)))
                .ForMember(dest => dest.InteriorFloor, opt => opt.ResolveUsing(OpaqueConstructionResolver(template => template.InteriorFl)))
                .ForMember(dest => dest.Roof, opt => opt.ResolveUsing(OpaqueConstructionResolver(template => template.RoofFl)))
                .ForMember(dest => dest.Window, opt => opt.ResolveUsing(WindowConstructionResolver(template => template.Glazing)))
                .ForMember(dest => dest.EquipmentDensity, opt => opt.MapFrom(src => src.EquipDnst))
                .ForMember(dest => dest.LightingDensity, opt => opt.MapFrom(src => src.LightDnst))
                .ForMember(dest => dest.OccupancyDensity, opt => opt.MapFrom(src => src.EquipDnst))
                .ForMember(dest => dest.CoolingCoeffOfPerf, opt => opt.MapFrom(src => src.CoolingCoP))
                .ForMember(dest => dest.CoolingSetpoint, opt => opt.MapFrom(src => src.CoolingSet))
                .ForMember(dest => dest.HeatingCoeffOfPerf, opt => opt.MapFrom(src => src.HeatingCoP))
                .ForMember(dest => dest.HeatingSetpoint, opt => opt.MapFrom(src => src.HeatingSet))
                .ForMember(dest => dest.NatVentMinRelHumidity, opt => opt.MapFrom(src => src.NatVMinRH))
                .ForMember(dest => dest.NatVentMinTempIn, opt => opt.MapFrom(src => src.NatVMinTin))
                .ForMember(dest => dest.NatVentMinTempOut, opt => opt.MapFrom(src => src.NatVMinTout))
                .ForMember(dest => dest.NatVentRate, opt => opt.MapFrom(src => src.NatVent))
                .ForMember(dest => dest.WaterTempSupply, opt => opt.MapFrom(src => src.WaterTempSup))
                .ForMember(dest => dest.BlindsOn, opt => opt.MapFrom(src => src.BlindOn))
                .ForMember(dest => dest.BlindTrans, opt => opt.MapFrom(src => src.BlindTrns))
                .ForMember(dest => dest.BlindType, opt => opt.MapFrom(src => src.BlindT))
                .ForMember(dest => dest.EquipmentSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.EquipSchd)))
                .ForMember(dest => dest.LightingSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.LightSchd)))
                .ForMember(dest => dest.OccupancySchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.OccupSchd)))
                .ForMember(dest => dest.HeatingSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.HeatingSchd)))
                .ForMember(dest => dest.CoolingSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.CoolingSchd)))
                .ForMember(dest => dest.MechVentSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.MechVentSchd)))
                .ForMember(dest => dest.NatVentSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.NatVentSchd)))
                .ForMember(dest => dest.BlindSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.BlindSchd)))
                .ForMember(dest => dest.WaterSchedule, opt => opt.ResolveUsing(YearScheduleResolver(template => template.WaterSchd)));

            Mapper
                .CreateMap<Legacy.BuildingTemplate, Core.BuildingTemplate>()
                .ForMember(dest => dest.Core, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Perimeter, opt => opt.MapFrom(src => src));

            // The mapping order matters: Referenced component types must be mapped before the types that reference them.
            // e.g. Materials before constructions before buildings
            Mapper
                .CreateMap<Legacy.Library, CoreLookupLibrary>()
                .ForMember(dest => dest.OpaqueMaterials, opt => opt.SetMappingOrder(10))
                .ForMember(dest => dest.GlazingMaterials, opt => opt.SetMappingOrder(10))
                .ForMember(dest => dest.GasMaterials, opt => opt.SetMappingOrder(10))
                .ForMember(dest => dest.OpaqueConstructions, opt => opt.SetMappingOrder(20))
                .ForMember(dest => dest.WindowConstructions, opt =>
                {
                    opt.SetMappingOrder(20);
                    opt.MapFrom(src => src.GlazingConstructions);
                })
                .ForMember(dest => dest.StructureDefinitions, opt =>
                {
                    opt.SetMappingOrder(20);
                    opt.MapFrom(src => src.StructureTypes);
                })
                .ForMember(dest => dest.DaySchedules, opt => opt.SetMappingOrder(30))
                .ForMember(dest => dest.WeekSchedules, opt => opt.SetMappingOrder(40))
                .ForMember(dest => dest.YearSchedules, opt => opt.SetMappingOrder(50))
                .ForMember(dest => dest.BuildingTemplates, opt => opt.SetMappingOrder(1000));
        }

        public static Core.Library Convert(Library legacyLib)
        {
            return Mapper.Map<CoreLookupLibrary>(legacyLib);
        }

        private static CoreLookupLibrary GetDestinationLibrary(ResolutionContext ctx)
        {
            if (ctx.Parent == null)
            {
                // Just using DestinationValue doesn't work (https://github.com/AutoMapper/AutoMapper/issues/873)
                var res = (CoreLookupLibrary)
                    ctx
                    .InstanceCache
                    .Single(kvp => kvp.Key.DestinationType == typeof(CoreLookupLibrary))
                    .Value;
                System.Diagnostics.Debug.Assert(res != null);
                return res;
            }
            else
            {
                return GetDestinationLibrary(ctx.Parent);
            }
        }

        private static Func<ResolutionResult, object> OpaqueConstructionResolver(Func<BuildingTemplate, string> sourceProp)
        {
            return res =>
            {
                var library = GetDestinationLibrary(res.Context);
                var lookup = library.OpaqueConstructionLookup;
                var legacyTemplate = (Legacy.BuildingTemplate)res.Context.SourceValue;
                var name = sourceProp(legacyTemplate);
                var newConstruction = default(Core.OpaqueConstruction);
                lookup.TryGetValue(name, out newConstruction);
                return newConstruction;
            };
        }

        private static IList<Core.MassRatios> ResolveMassRatios(ResolutionResult res)
        {
            var library = GetDestinationLibrary(res.Context);
            var lookup = library.OpaqueMaterialLookup;
            var legacyStructure = (Legacy.StructureType)res.Context.SourceValue;
            return
                legacyStructure
                .Materials
                .Select(comp =>
                {
                    var newMat = default(Core.OpaqueMaterial);
                    if (lookup.TryGetValue(comp.MaterialName, out newMat))
                    {
                        return new Core.MassRatios()
                        {
                            Material = newMat,
                            NormalRatio = comp.QuantRatio,
                            HighLoadRatio = comp.QuantRatioHigh
                        };
                    }
                    else { return null; }
                })
                .ToList();
        }

        private static IList<Core.MaterialLayer<Core.OpaqueMaterial>> ResolveOpaqueMaterialLayers(ResolutionResult res)
        {
            var library = GetDestinationLibrary(res.Context);
            var lookup = library.OpaqueMaterialLookup;
            var legacyConstruction = (Legacy.OpaqueConstruction)res.Context.SourceValue;
            return
                legacyConstruction
                .Layers
                .Select(layer =>
                {
                    var newMat = default(Core.OpaqueMaterial);
                    if (lookup.TryGetValue(layer.MaterialName, out newMat))
                    {
                        return new Core.MaterialLayer<Core.OpaqueMaterial>()
                        {
                            Material = newMat,
                            Thickness = layer.Thickness
                        };
                    }
                    else { return null; }
                })
                .ToList();
        }

        private static IList<Core.MaterialLayer<Core.WindowMaterialBase>> ResolveWindowMaterialLayers(ResolutionResult res)
        {
            var library = GetDestinationLibrary(res.Context);
            var lookup = library.WindowMaterialLookup;
            var legacyConstruction = (Legacy.GlazingConstruction)res.Context.SourceValue;
            return
                legacyConstruction
                .Layers
                .Select(layer =>
                {
                    var newMat = default(Core.WindowMaterialBase);
                    if (lookup.TryGetValue(layer.MaterialName, out newMat))
                    {
                        return new Core.MaterialLayer<Core.WindowMaterialBase>()
                        {
                            Material = newMat,
                            Thickness = layer.Thickness
                        };
                    }
                    else { return null; }
                })
                .ToList();
        }

        private static Core.DaySchedule[] ResolveWeekScheduleDays(ResolutionResult res)
        {
            var library = GetDestinationLibrary(res.Context);
            var lookup = library.DayScheduleLookup;
            var legacyWeek = (Legacy.WeekSchedule)res.Context.SourceValue;
            return
                legacyWeek
                .Days
                .Select(dayName =>
                {
                    var newDay = default(Core.DaySchedule);
                    lookup.TryGetValue(dayName, out newDay);
                    return newDay;
                })
                .ToArray();
        }
        
        private static IList<Core.YearSchedulePart> ResolveYearScheduleParts(ResolutionResult res)
        {
            var library = GetDestinationLibrary(res.Context);
            var lookup = library.WeekScheduleLookup;
            var legacyYear = (Legacy.YearSchedule)res.Context.SourceValue;
            var weekCount = legacyYear.WeekScheduleNames.Count;
            if (legacyYear.MonthFrom.Count != weekCount ||
                legacyYear.DayFrom.Count != weekCount ||
                legacyYear.MonthTill.Count != weekCount ||
                legacyYear.DayTill.Count != weekCount)
            {
                return null;
            }
            var newWeeks =
                legacyYear
                .WeekScheduleNames
                .Select(name =>
                {
                    var newWeek = default(Core.WeekSchedule);
                    lookup.TryGetValue(name, out newWeek);
                    return newWeek;
                })
                .ToArray();
            if (newWeeks.Any(w => w == null)) { return null; }
            var parts = new List<Core.YearSchedulePart>();
            for (var i = 0; i < weekCount; ++i)
            {
                parts.Add(new Core.YearSchedulePart()
                {
                    Schedule = newWeeks[i],
                    FromDay = legacyYear.DayFrom[i],
                    FromMonth = legacyYear.MonthFrom[i],
                    ToDay = legacyYear.DayTill[i],
                    ToMonth = legacyYear.MonthTill[i]
                });
            }
            return parts;
        }

        private static Func<ResolutionResult, Core.WindowConstruction> WindowConstructionResolver(Func<BuildingTemplate, string> sourceProp)
        {
            return res =>
            {
                var library = GetDestinationLibrary(res.Context);
                var lookup = library.WindowConstructionLookup;
                var legacyTemplate = (Legacy.BuildingTemplate)res.Context.SourceValue;
                var name = sourceProp(legacyTemplate);
                var newConstruction = default(Core.WindowConstruction);
                lookup.TryGetValue(name, out newConstruction);
                return newConstruction;
            };
        }

        private static Func<ResolutionResult, Core.YearSchedule> YearScheduleResolver(Func<BuildingTemplate, string> sourceProp)
        {
            return res =>
            {
                var library = GetDestinationLibrary(res.Context);
                var lookup = library.YearScheduleLookup;
                var legacyTemplate = (Legacy.BuildingTemplate)res.Context.SourceValue;
                var name = sourceProp(legacyTemplate);
                var newSchedule = default(Core.YearSchedule);
                lookup.TryGetValue(name, out newSchedule);
                return newSchedule;
            };
        }
    }
}
