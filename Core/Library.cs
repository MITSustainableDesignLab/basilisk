using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Newtonsoft.Json;

namespace Basilisk.Core
{
    [DataContract]
    public class Library
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Newtonsoft.Json.Formatting.Indented
        };

        [DataMember]
        public ICollection<BuildingTemplate> BuildingTemplates { get; set; } = new List<BuildingTemplate>();

        #region Constructions and materials
        public IEnumerable<WindowMaterialBase> AllWindowMaterials => GlazingMaterials.Cast<WindowMaterialBase>().Concat(GasMaterials);

        [DataMember]
        public ICollection<GasMaterial> GasMaterials { get; set; } = new List<GasMaterial>();

        [DataMember]
        public ICollection<GlazingMaterial> GlazingMaterials { get; set; } = new List<GlazingMaterial>();

        [DataMember]
        public ICollection<OpaqueConstruction> OpaqueConstructions { get; set; } = new List<OpaqueConstruction>();

        [DataMember]
        public ICollection<OpaqueMaterial> OpaqueMaterials { get; set; } = new List<OpaqueMaterial>();

        [DataMember]
        public ICollection<WindowConstruction> WindowConstructions { get; set; } = new List<WindowConstruction>();

        [DataMember]
        public ICollection<StructureInformation> StructureDefinitions { get; set; } = new List<StructureInformation>();
        #endregion

        #region Schedules
        [DataMember]
        public ICollection<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();

        [DataMember]
        public ICollection<WeekSchedule> WeekSchedules { get; set; } = new List<WeekSchedule>();

        [DataMember]
        public ICollection<YearSchedule> YearSchedules { get; set; } = new List<YearSchedule>();
        #endregion

        public static Library FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Library>(json, JsonSettings);
        }

        public static Task<Library> FromJsonAsync(string json)
        {
            using (var reader = new StringReader(json))
            {
                return JsonConvert.DeserializeObjectAsync<Library>(json, JsonSettings);
            }
        }

        public static Library FromXml(string path)
        {
            using (var file = File.OpenRead(path))
            using (var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas()))
            {
                var deserializer = new DataContractSerializer(typeof(Library));
                return (Library)deserializer.ReadObject(reader);
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, JsonSettings);
        }

        public Task<string> ToJsonAsync()
        {
            return JsonConvert.SerializeObjectAsync(this, Newtonsoft.Json.Formatting.Indented, JsonSettings);
        }

        public string ToXml()
        {
            using (var stringWriter = new StringWriter())
            using (var xml = XmlWriter.Create(stringWriter))
            {
                var serializer = new DataContractSerializer(typeof(Library));
                serializer.WriteObject(xml, this);
                return stringWriter.ToString();
            }
        }

        public IEnumerable<LibraryComponent> GetOrphanedComponents()
        {
            var known =
                OpaqueMaterials
                .Cast<LibraryComponent>()
                .Concat(GlazingMaterials)
                .Concat(GasMaterials)
                .Concat(OpaqueConstructions)
                .Concat(WindowConstructions)
                .Concat(StructureDefinitions)
                .Concat(DaySchedules)
                .Concat(WeekSchedules)
                .Concat(YearSchedules)
                .Concat(BuildingTemplates);
            return
                known
                .SelectMany(c => c.ReferencedComponents)
                .Except(known);
        }
    }
}
