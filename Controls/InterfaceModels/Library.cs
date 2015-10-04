using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

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
                .IncludeBase<LibraryComponent, Core.LibraryComponent>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Parts, opt => opt.Ignore());

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
                .CreateMap<ConstructionBase, Core.ConstructionBase>()
                .IncludeBase<LibraryComponent, Core.LibraryComponent>();
            Mapper
                .CreateMap<OpaqueConstruction, Core.OpaqueConstruction>()
                .IncludeBase<ConstructionBase, Core.ConstructionBase>();
            Mapper
                .CreateMap<WindowConstruction, Core.WindowConstruction>()
                .IncludeBase<ConstructionBase, Core.ConstructionBase>();
        }

        public Library()
        {
            GasMaterials = new List<LibraryComponent>();
            GlazingMaterials = new List<LibraryComponent>();
            OpaqueConstructions = new List<LibraryComponent>();
            OpaqueMaterials = new List<LibraryComponent>();
            WindowConstructions = new List<LibraryComponent>();
            Schedules = new List<LibraryComponent>();
        }

        public IEnumerable<LibraryComponent> AllComponents =>
            GasMaterials
            .Concat(GlazingMaterials)
            .Concat(OpaqueMaterials)
            .Concat(OpaqueConstructions)
            .Concat(WindowConstructions)
            .Concat(Schedules);

        public ICollection<LibraryComponent> GasMaterials { get; set; }
        public ICollection<LibraryComponent> GlazingMaterials { get; set; }
        public ICollection<LibraryComponent> OpaqueMaterials { get; set; }

        public ICollection<LibraryComponent> OpaqueConstructions { get; set; }
        public ICollection<LibraryComponent> WindowConstructions { get; set; }

        public ICollection<LibraryComponent> Schedules { get; set; }

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
                .Select(src => BuildConstruction<Core.OpaqueConstruction, OpaqueConstruction, Core.OpaqueMaterial>(src, opaqueMatDict))
                .ToList();
            var windowConstructions =
                sourceLib
                .WindowConstructions
                .Select(src => BuildConstruction<Core.WindowConstruction, WindowConstruction, Core.WindowMaterialBase>(src, windowMatDict))
                .ToList();

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
                });

            var allSchedules =
                days
                .Values
                .Cast<LibraryComponent>()
                .Concat(weeks.Values)
                .Concat(years)
                .ToList();

            return new Library()
            {
                OpaqueMaterials = opaqueMats,
                GlazingMaterials = glazingMats,
                GasMaterials = gasMats,
                OpaqueConstructions = opaqueConstructions,
                WindowConstructions = windowConstructions,
                Schedules = allSchedules
            };
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
                DaySchedules = new List<Core.DaySchedule>(),
                WeekSchedules = new List<Core.WeekSchedule>(),
                YearSchedules = new List<Core.YearSchedule>()
            };
            // There's a dummy layer at the end of each construction because of the DataGrid "new row" row.
            // There's a probably a better way to avoid this, but for now I'll just strip them out. Also,
            // we need to re-wire materials.
            var knownOpaqueConstructions = newLib.OpaqueMaterials.ToDictionary(c => c.Name);
            var knownWindowConstructions = newLib.AllWindowMaterials.ToDictionary(c => c.Name);
            foreach (var c in newLib.OpaqueConstructions)
            {
                c.Layers =
                    c
                    .Layers
                    .Where(layer => layer.Material != null)
                    .Select(layer => new Core.MaterialLayer<Core.OpaqueMaterial>()
                    {
                        Material = knownOpaqueConstructions[layer.Material.Name],
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
                        Material = knownWindowConstructions[layer.Material.Name],
                        Thickness = layer.Thickness
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

            return newLib;
        }

        private static LibraryComponent BuildConstruction<SourceT, DestT, MaterialT>(SourceT src, Dictionary<string, LibraryComponent> matDict)
            where SourceT : Core.LayeredConstruction<MaterialT>
            where DestT : ConstructionBase
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
    }
}
