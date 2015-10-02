using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using CommandLine;

using Core = Basilisk.Core;
using Legacy = Basilisk.Legacy;

namespace Basilisk.LegacyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var opts = new Options();
            if (!Parser.Default.ParseArguments(args, opts) || opts.HasMultipleInputFiles)
            {
                return;
            }
            using (var reader = new StreamReader(opts.InputFile))
            {
                var serializer = new XmlSerializer(typeof(Legacy.Library));
                var legacy = (Legacy.Library)serializer.Deserialize(reader);
                var newLib = Legacy.Conversion.Convert(legacy);
                Console.WriteLine("Mapped {0} of {1} opaque materials", newLib.OpaqueMaterials.Count, legacy.OpaqueMaterials.Count);
                Console.WriteLine("Mapped {0} of {1} glazing materials", newLib.GlazingMaterials.Count, legacy.GlazingMaterials.Count);
                Console.WriteLine("Mapped {0} of {1} gas materials", newLib.GasMaterials.Count, legacy.GasMaterials.Count);
                Console.WriteLine("Mapped {0} of {1} opaque constructions", newLib.OpaqueConstructions.Count, legacy.OpaqueConstructions.Count);
                Console.WriteLine("Mapped {0} of {1} window constructions", newLib.WindowConstructions.Count, legacy.GlazingConstructions.Count);
            }
        }
    }
}
