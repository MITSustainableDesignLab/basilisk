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

using JsonFormatting = Newtonsoft.Json.Formatting;

namespace Basilisk.Core
{
    [DataContract]
    public class Library
    {
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

        #region Zone stuff
        [DataMember]
        public ICollection<DomesticHotWaterSettings> DomesticHotWaterSettings { get; set; } = new List<DomesticHotWaterSettings>();

        [DataMember]
        public ICollection<ZoneVentilation> VentilationSettings { get; set; }

        [DataMember]
        public ICollection<WindowSettings> WindowSettings { get; set; } = new List<WindowSettings>();

        [DataMember]
        public ICollection<ZoneConditioning> ZoneConditionings { get; set; } = new List<ZoneConditioning>();

        [DataMember]
        public ICollection<ZoneConstructions> ZoneConstructionSets { get; set; } = new List<ZoneConstructions>();

        [DataMember]
        public ICollection<ZoneLoads> ZoneLoads { get; set; } = new List<ZoneLoads>();

        [DataMember]
        public ICollection<ZoneDefinition> Zones { get; set; } = new List<ZoneDefinition>();
        #endregion

        public static Library FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Library>(json);
        }

        public static Task<Library> FromJsonAsync(string json)
        {
            return JsonConvert.DeserializeObjectAsync<Library>(json);
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
            if (OrphanedComponents().Any())
            {
                throw new InvalidOperationException("The component library has at least one orphaned component and cannot be serialized.");
            }
            return JsonConvert.SerializeObject(this, JsonFormatting.Indented);
        }

        public Task<string> ToJsonAsync()
        {
            if (OrphanedComponents().Any())
            {
                throw new InvalidOperationException("The component library has at least one orphaned component and cannot be serialized.");
            }
            return JsonConvert.SerializeObjectAsync(this, JsonFormatting.Indented);
        }

        public string ToXml()
        {
            if (OrphanedComponents().Any())
            {
                throw new InvalidOperationException("The component library has at least one orphaned component and cannot be serialized.");
            }
            using (var stringWriter = new StringWriter())
            using (var xml = XmlWriter.Create(stringWriter))
            {
                var serializer = new DataContractSerializer(typeof(Library));
                serializer.WriteObject(xml, this);
                return stringWriter.ToString();
            }
        }

        public IEnumerable<LibraryComponent> OrphanedComponents()
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
                .Concat(DomesticHotWaterSettings)
                .Concat(WindowSettings)
                .Concat(ZoneConditionings)
                .Concat(ZoneConstructionSets)
                .Concat(ZoneLoads)
                .Concat(VentilationSettings)
                .Concat(Zones)
                .Concat(BuildingTemplates);
            return
                known
                .SelectMany(c => c.ReferencedComponents)
                .Except(known);
        }
    }
}
