using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using AutoMapper;

using Basilisk.Core;

using Legacy = Basilisk.Core.Legacy;

namespace Basilisk.LegacyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 || (args[0] != "xml" && args[0] != "js"))
            {
                Console.WriteLine("Usage: legacyconvert <xml|js> [input-path]");
                return;
            }

            using (var reader = new StreamReader(args[1]))
            {
                var serializer = new XmlSerializer(typeof(Legacy.Library));
                var legacy = (Legacy.Library)serializer.Deserialize(reader);
               
                Mapper
                    .CreateMap<Legacy.BaseMaterial, MaterialBase>()
                    .ForMember(dest => dest.EmbodiedCarbonStdDev, opt => opt.MapFrom(src => src.ECStandardDev))
                    .ForMember(dest => dest.EmbodiedEnergyStdDev, opt => opt.MapFrom(src => src.EEStandardDev))
                    .ForMember(dest => dest.SubstitutionTimestep, opt => opt.MapFrom(src => src.SubstituionTimeStep))
                    .ForMember(dest => dest.SubstitutionRatePattern, opt => opt.MapFrom(src => src.SubstituionRatePattern));
                Mapper
                    .CreateMap<Legacy.OpaqueMaterial, OpaqueMaterial>()
                    .IncludeBase<Legacy.BaseMaterial, MaterialBase>();
                Mapper
                    .CreateMap<Legacy.GlazingMaterial, GlazingMaterial>()
                    .IncludeBase<Legacy.BaseMaterial, MaterialBase>();
                Mapper
                    .CreateMap<Legacy.GasMaterial, GasMaterial>();

                Mapper.CreateMap<Legacy.Library, Library>();

                var newLib = Mapper.Map<Library>(legacy);
                Console.WriteLine("Mapped {0} of {1} opaque materials", newLib.OpaqueMaterials.Count, legacy.OpaqueMaterials.Count);
                Console.WriteLine("Mapped {0} of {1} glazing materials", newLib.GlazingMaterials.Count, legacy.GlazingMaterials.Count);
                Console.WriteLine("Mapped {0} of {1} gas materials", newLib.GasMaterials.Count, legacy.GasMaterials.Count);
            }
        }
    }
}
