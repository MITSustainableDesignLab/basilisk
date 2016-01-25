using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

namespace Basilisk.Archsim
{
    public static class Conversion
    {
        static Conversion()
        {
            Mapper
                .CreateMap<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comments));

            Mapper
                .CreateMap<Core.ConstructionBase, ArchsimLib.BaseConstruction>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.Life, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.OpaqueConstruction, ArchsimLib.OpaqueConstruction>()
                .IncludeBase<Core.ConstructionBase, ArchsimLib.BaseConstruction>()
                .ForMember(dest => dest.Layers, opt => opt.Ignore());
            Mapper
                .CreateMap<Core.WindowConstruction, ArchsimLib.GlazingConstruction>()
                .IncludeBase<Core.ConstructionBase, ArchsimLib.BaseConstruction>()
                .ForMember(dest => dest.Layers, opt => opt.Ignore());

            Mapper
                .CreateMap<Core.ZoneConditioning, ArchsimLib.ZoneConditioning>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.CoolingLimitType, opt => opt.ResolveUsing(src => Enum.Parse(typeof(ArchsimLib.IdealSystemLimit), src.CoolingLimitType.ToString(), ignoreCase: true)))
                .ForMember(dest => dest.CoolingSchedule, opt => opt.ResolveUsing(src => src.CoolingSchedule.Name))
                .ForMember(dest => dest.CoolIsOn, opt => opt.MapFrom(src => src.IsCoolingOn))
                .ForMember(dest => dest.EconomizerType, opt => opt.ResolveUsing(src => Enum.Parse(typeof(ArchsimLib.EconomizerItem), src.EconomizerType.ToString(), ignoreCase: true)))
                .ForMember(dest => dest.HeatingLimitType, opt => opt.ResolveUsing(src => Enum.Parse(typeof(ArchsimLib.IdealSystemLimit), src.HeatingLimitType.ToString(), ignoreCase: true)))
                .ForMember(dest => dest.HeatingSchedule, opt => opt.ResolveUsing(src => src.HeatingSchedule.Name))
                .ForMember(dest => dest.HeatIsOn, opt => opt.MapFrom(src => src.IsHeatingOn))
                .ForMember(dest => dest.HeatRecoveryType, opt => opt.ResolveUsing(src => Enum.Parse(typeof(ArchsimLib.HeatRecoveryItem), src.HeatRecoveryType.ToString(), ignoreCase: true)))
                .ForMember(dest => dest.MechVentIsOn, opt => opt.MapFrom(src => src.IsMechVentOn))
                .ForMember(dest => dest.MechVentSchedule, opt => opt.ResolveUsing(src => src.MechVentSchedule.Name))
                .ForMember(dest => dest.MinFreshAirArea, opt => opt.MapFrom(src => src.MinFreshAirPerArea))
                .ForMember(dest => dest.MinFreshAirPerson, opt => opt.MapFrom(src => src.MinFreshAirPerPerson));
            Mapper
                .CreateMap<Core.DomesticHotWaterSettings, ArchsimLib.DomHotWater>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.WaterSchedule, opt => opt.ResolveUsing(src => src.WaterSchedule.Name));
            Mapper
                .CreateMap<Core.ZoneLoads, ArchsimLib.ZoneLoad>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.DimmingType, opt => opt.ResolveUsing(src => Enum.Parse(typeof(ArchsimLib.DimmingItem), src.DimmingType.ToString(), ignoreCase: true)))
                .ForMember(dest => dest.EquipmentAvailibilitySchedule, opt => opt.ResolveUsing(src => src.EquipmentAvailabilitySchedule.Name))
                .ForMember(dest => dest.EquipmentIsOn, opt => opt.MapFrom(src => src.IsEquipmentOn))
                .ForMember(dest => dest.LightsAvailibilitySchedule, opt => opt.ResolveUsing(src => src.LightsAvailabilitySchedule.Name))
                .ForMember(dest => dest.LightsIsOn, opt => opt.MapFrom(src => src.IsLightingOn))
                .ForMember(dest => dest.OccupancySchedule, opt => opt.ResolveUsing(src => src.OccupancySchedule.Name))
                .ForMember(dest => dest.PeopleIsOn, opt => opt.MapFrom(src => src.IsPeopleOn));
            Mapper
                .CreateMap<Core.ZoneConstructions, ArchsimLib.ZoneConstruction>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.FacadeConstruction, opt => opt.ResolveUsing(src => src.Facade.Name))
                .ForMember(dest => dest.FacadeIsAdiabatic, opt => opt.MapFrom(src => src.IsFacadeAdiabatic))
                .ForMember(dest => dest.GroundConstruction, opt => opt.ResolveUsing(src => src.Ground.Name))
                .ForMember(dest => dest.GroundIsAdiabatic, opt => opt.MapFrom(src => src.IsGroundAdiabatic))
                .ForMember(dest => dest.PartitionConstruction, opt => opt.ResolveUsing(src => src.Partition.Name))
                .ForMember(dest => dest.PartitionIsAdiabatic, opt => opt.MapFrom(src => src.IsPartitionAdiabatic))
                .ForMember(dest => dest.RoofConstruction, opt => opt.ResolveUsing(src => src.Roof.Name))
                .ForMember(dest => dest.RoofIsAdiabatic, opt => opt.MapFrom(src => src.IsRoofAdiabatic))
                .ForMember(dest => dest.SlabConstruction, opt => opt.ResolveUsing(src => src.Slab.Name))
                .ForMember(dest => dest.SlabIsAdiabatic, opt => opt.MapFrom(src => src.IsSlabAdiabatic));
            Mapper
                .CreateMap<Core.ZoneVentilation, ArchsimLib.ZoneVentilation>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.AFN, opt => opt.MapFrom(src => src.Afn))
                .ForMember(dest => dest.BuoyancyDrivenIsOn, opt => opt.MapFrom(src => src.IsBuoyancyOn))
                .ForMember(dest => dest.InfiltrationAch, opt => opt.MapFrom(src => src.Infiltration))
                .ForMember(dest => dest.InfiltrationIsOn, opt => opt.MapFrom(src => src.IsInfiltrationOn))
                .ForMember(dest => dest.NatVentIsOn, opt => opt.MapFrom(src => src.IsNatVentOn))
                .ForMember(dest => dest.NatVentMaxOutAirTemp, opt => opt.MapFrom(src => src.NatVentMaxOutdoorAirTemp))
                .ForMember(dest => dest.NatVentMaxRelHum, opt => opt.MapFrom(src => src.NatVentMaxRelHumidity))
                .ForMember(dest => dest.NatVentMinOutAirTemp, opt => opt.MapFrom(src => src.NatVentMinOutdoorAirTemp))
                .ForMember(dest => dest.NatVentSchedule, opt => opt.ResolveUsing(src => src.NatVentSchedule.Name))
                .ForMember(dest => dest.NatVentSetPoint, opt => opt.MapFrom(src => src.NatVentZoneTempSetpoint))
                .ForMember(dest => dest.ScheduledVentilationSchedule, opt => opt.ResolveUsing(src => src.ScheduledVentilationSchedule.Name))
                .ForMember(dest => dest.ScheduledVentilationSetPoint, opt => opt.MapFrom(src => src.ScheduledVentilationSetpoint))
                .ForMember(dest => dest.SchedVentIsOn, opt => opt.MapFrom(src => src.IsScheduledVentilationOn))
                .ForMember(dest => dest.WindDrivenIsOn, opt => opt.MapFrom(src => src.IsWindOn));

            Mapper
                .CreateMap<Core.ZoneDefinition, ArchsimLib.ZoneDefinition>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.SurfaceConvectionModelInside, opt => opt.Ignore())
                .ForMember(dest => dest.SurfaceConvectionModelOutside, opt => opt.Ignore())
                .ForMember(dest => dest.ZoneMultiplier, opt => opt.Ignore())
                .ForMember(dest => dest.ZonePriority, opt => opt.Ignore())
                .ForMember(dest => dest.InternalMassConstruction, opt => opt.ResolveUsing(zone => zone.InternalMassConstruction.Name))
                .ForMember(dest => dest.InternalMassExposedAreaPerArea, opt => opt.MapFrom(src => src.InternalMassExposedPerFloorArea))
                .ForMember(dest => dest.DomHotWater, opt => opt.MapFrom(src => src.DomesticHotWater))
                .ForMember(dest => dest.Materials, opt => opt.MapFrom(src => src.Constructions));

            Mapper
                .CreateMap<Core.WindowSettings, ArchsimLib.WindowSettings>()
                .IncludeBase<Core.LibraryComponent, ArchsimLib.LibraryComponent>()
                .ForMember(dest => dest.AFN_DISCHARGE_C, opt => opt.MapFrom(src => src.AfnDischargeC))
                .ForMember(dest => dest.AFN_TEMP_SETPT, opt => opt.MapFrom(src => src.AfnTempSetpoint))
                .ForMember(dest => dest.AFN_WIN_AVAIL, opt => opt.ResolveUsing(src => src.AfnWindowAvailability.Name))
                .ForMember(dest => dest.Construction, opt => opt.ResolveUsing(src => src.Construction.Name))
                .ForMember(dest => dest.ShadingSystemAvailibilitySchedule, opt => opt.ResolveUsing(src => src.ShadingSystemAvailabilitySchedule.Name))
                .ForMember(dest => dest.ShadingSystemIsOn, opt => opt.MapFrom(src => src.IsShadingSystemOn))
                .ForMember(dest => dest.ShadingSystemSetPoint, opt => opt.MapFrom(src => src.ShadingSystemSetpoint))
                .ForMember(dest => dest.ShadingSystemType, opt => opt.ResolveUsing(src => Enum.Parse(typeof(ArchsimLib.ShadingType), src.ShadingSystemType.ToString(), ignoreCase: true)))
                .ForMember(dest => dest.ZoneMixingAvailibilitySchedule, opt => opt.ResolveUsing(src => src.ZoneMixingAvailabilitySchedule.Name))
                .ForMember(dest => dest.ZoneMixingIsOn, opt => opt.MapFrom(src => src.IsZoneMixingOn));

            Mapper.AssertConfigurationIsValid();
        }

        public static ArchsimLib.Library Convert(Core.Library lib)
        {
            var res = new ArchsimLib.Library();

            res.TimeStamp = DateTime.Now;

            res.DaySchedules = lib.DaySchedules.Select(Convert).ToList();

            var allDays =
                res
                .DaySchedules
                .ToDictionary(d => d.Name);
            Func<Core.DaySchedule, ArchsimLib.DaySchedule> getDay = coreDay =>
            {
                var day = default(ArchsimLib.DaySchedule);
                allDays.TryGetValue(coreDay.Name, out day);
                return day;
            };
            res.WeekSchedules = lib.WeekSchedules.Select(w => Convert(w, getDay)).ToList();

            var allWeeks =
                res
                .WeekSchedules
                .ToDictionary(w => w.Name);
            Func<Core.WeekSchedule, ArchsimLib.WeekSchedule> getWeek = coreWeek =>
            {
                var week = default(ArchsimLib.WeekSchedule);
                allWeeks.TryGetValue(coreWeek.Name, out week);
                return week;
            };
            res.YearSchedules = lib.YearSchedules.Select(y => Convert(y, getWeek)).ToList();

            res.GasMaterials = lib.GasMaterials.ToList();
            res.GlazingMaterials = lib.GlazingMaterials.ToList();
            res.OpaqueMaterials = lib.OpaqueMaterials.ToList();

            var allOpaqueMats =
                res
                .OpaqueMaterials
                .ToDictionary(mat => mat.Name);
            var allWindowMats =
                res
                .GasMaterials
                .Cast<ArchsimLib.WindowMaterialBase>()
                .Concat(res.GlazingMaterials)
                .ToDictionary(mat => mat.Name);

            res.GlazingConstructions = lib.WindowConstructions.Select(Convert).ToList();
            res.OpaqueConstructions = lib.OpaqueConstructions.Select(Convert).ToList();

            res.DomHotWaters = res.ZoneDefinitions.Select(z => z.DomHotWater).ToList();
            res.ZoneConditionings = res.ZoneDefinitions.Select(z => z.Conditioning).ToList();
            res.ZoneConstructions = res.ZoneDefinitions.Select(z => z.Materials).ToList();
            res.ZoneLoads = res.ZoneDefinitions.Select(z => z.Loads).ToList();
            res.ZoneVentilations = res.ZoneDefinitions.Select(z => z.Ventilation).ToList();

            return res;
        }

        public static ArchsimLib.DaySchedule Convert(Core.DaySchedule day) =>
            new ArchsimLib.DaySchedule()
            {
                Name = day.Name,
                DataSource = day.DataSource,
                Comment = day.Comments,
                Type = day.Type,
                Values = day.Values.ToList()
            };

        public static ArchsimLib.DomHotWater Convert(Core.DomesticHotWaterSettings dhw) =>
            Mapper.Map<ArchsimLib.DomHotWater>(dhw);

        public static ArchsimLib.GlazingConstruction Convert(Core.WindowConstruction window)
        {
            var res = Mapper.Map<ArchsimLib.GlazingConstruction>(window);
            res.Layers =
                window
                .Layers
                .Select(layer => new ArchsimLib.Layer<ArchsimLib.WindowMaterialBase>(layer.Thickness, layer.Material))
                .ToList();
            return res;
        }

        public static ArchsimLib.OpaqueConstruction Convert(Core.OpaqueConstruction c)
        {
            var res = Mapper.Map<ArchsimLib.OpaqueConstruction>(c);
            res.Layers =
                c
                .Layers
                .Select(layer => new ArchsimLib.Layer<ArchsimLib.OpaqueMaterial>(layer.Thickness, layer.Material))
                .ToList();
            return res;
        }

        public static ArchsimLib.WeekSchedule Convert(Core.WeekSchedule week, Func<Core.DaySchedule, ArchsimLib.DaySchedule> getArchsimDay)
        {
            var res = new ArchsimLib.WeekSchedule()
            {
                Name = week.Name,
                DataSource = week.DataSource,
                Comment = week.Comments,
                Type = week.Type,
                Days = week.Days.Select(getArchsimDay).ToArray()
            };
            // TODO: wtf is FromTo used for
            return res;
        }

        public static ArchsimLib.WindowSettings Convert(Core.WindowSettings w) =>
            Mapper.Map<ArchsimLib.WindowSettings>(w);

        public static ArchsimLib.YearSchedule Convert(Core.YearSchedule year, Func<Core.WeekSchedule, ArchsimLib.WeekSchedule> getArchsimWeek)
        {
            var res = new ArchsimLib.YearSchedule()
            {
                Name = year.Name,
                DataSource = year.DataSource,
                Comment = year.Comments
            };
            res.DayFrom = year.Parts.Select(part => part.FromDay).ToList();
            res.DayTill = year.Parts.Select(part => part.ToDay).ToList();
            res.MonthFrom = year.Parts.Select(part => part.FromMonth).ToList();
            res.MonthTill = year.Parts.Select(part => part.ToMonth).ToList();
            res.WeekSchedules = year.Parts.Select(part => getArchsimWeek(part.Schedule)).ToList();
            return res;
        }

        public static ArchsimLib.ZoneDefinition Convert(Core.ZoneDefinition zone) =>
            Mapper.Map<ArchsimLib.ZoneDefinition>(zone);

        public static void VerifyZoneConstructions(ArchsimLib.Library lib)
        {
            Action<ArchsimLib.ZoneDefinition, string> check = (z, cName) =>
            {
                if (!lib.OpaqueConstructions.Any(c => c.Name == cName))
                {
                    throw new Exception($"The Archsim library does not contain opaque construction '{cName}', which is referenced by zone definition '{z.Name}'.");
                }
            };
            foreach (var z in lib.ZoneDefinitions)
            {
                check(z, z.Materials.FacadeConstruction);
                check(z, z.Materials.GroundConstruction);
                check(z, z.Materials.PartitionConstruction);
                check(z, z.Materials.RoofConstruction);
                check(z, z.Materials.SlabConstruction);
            }
        }
    }
}
