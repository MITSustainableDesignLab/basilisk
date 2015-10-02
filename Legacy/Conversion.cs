using System;
using System.Collections.Generic;
using System.Linq;
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
                .CreateMap<Legacy.OpaqueMaterial, Core.OpaqueMaterial>();
            Mapper
                .CreateMap<Legacy.GlazingMaterial, Core.GlazingMaterial>();
            Mapper
                .CreateMap<Legacy.GasMaterial, Core.GasMaterial>();

            Mapper
                .CreateMap<Legacy.OpaqueConstruction, Core.OpaqueConstruction>()
                .ForMember(dest => dest.Layers, opt => opt.Ignore());
            Mapper
                .CreateMap<Legacy.GlazingConstruction, Core.WindowConstruction>()
                .ForMember(dest => dest.Layers, opt => opt.Ignore());

            Mapper
                .CreateMap<Library, Core.Library>()
                .ForMember(dest => dest.OpaqueConstructions, opt => opt.Ignore());
        }

        public static Core.Library Convert(Library legacyLib)
        {
            var newLib = Mapper.Map<Core.Library>(legacyLib);
            newLib.GasMaterials = newLib.GasMaterials.Where(m => m.Name != null).ToList();
            newLib.GlazingMaterials = newLib.GlazingMaterials.Where(m => m.Name != null).ToList();
            newLib.OpaqueMaterials = newLib.OpaqueMaterials.Where(m => m.Name != null).ToList();
            newLib.OpaqueConstructions = Convert<
                Legacy.BaseConstruction<Legacy.OpaqueLayer>,
                Legacy.OpaqueLayer,
                Core.OpaqueConstruction,
                Core.OpaqueMaterial>(legacyLib.OpaqueConstructions, newLib.OpaqueMaterials).ToList();
            newLib.WindowConstructions = Convert<
                Legacy.BaseConstruction<Legacy.GlazingLayer>,
                Legacy.GlazingLayer,
                Core.WindowConstruction,
                Core.WindowMaterialBase>(legacyLib.GlazingConstructions, newLib.AllWindowMaterials).ToList();
            return newLib;
        }

        public static IEnumerable<NewConstructionT> Convert<LegacyConstructionT, LegacyLayerT, NewConstructionT, NewMaterialT>(
            IEnumerable<LegacyConstructionT> constructions,
            IEnumerable<NewMaterialT> materials)
            where LegacyConstructionT : Legacy.BaseConstruction<LegacyLayerT>
            where LegacyLayerT : Legacy.BaseLayer
            where NewConstructionT : Core.LayeredConstruction<NewMaterialT>
            where NewMaterialT : Core.MaterialBase
        {
            var matLookup = materials.ToDictionary(m => m.Name);
            foreach (var legacyConstruction in constructions)
            {
                var newLayers =
                    legacyConstruction
                    .Layers
                    .Select(layer =>
                    {
                        NewMaterialT mat = null;
                        matLookup.TryGetValue(layer.MaterialName, out mat);
                        return new Core.MaterialLayer<NewMaterialT>()
                        {
                            Material = mat,
                            Thickness = layer.Thickness
                        };
                    })
                    .ToList();
                if (!newLayers.Any(layer => layer.Material == null))
                {
                    var c = Mapper.Map<NewConstructionT>(legacyConstruction);
                    c.Layers = newLayers;
                    yield return c;
                }
            }
        }
    }
}
