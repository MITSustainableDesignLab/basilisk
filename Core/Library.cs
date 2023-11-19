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
        [DataMember(Order = 100)]
        public ICollection<BuildingTemplate> BuildingTemplates { get; set; } = new List<BuildingTemplate>();

        #region Constructions and materials
        public IEnumerable<WindowMaterialBase> AllWindowMaterials => GlazingMaterials.Cast<WindowMaterialBase>().Concat(GasMaterials);

        // Gas materials and glazing materials must be serialized first so that they're properly typed
        // when they're deserialized. (If they're serialized as part of another component, the deserializer
        // will only know that they're WindowMaterialBases and won't have a concrete type.)
        [DataMember(Order = 1)]
        public ICollection<GasMaterial> GasMaterials { get; set; } = new List<GasMaterial>();

        [DataMember(Order = 1)]
        public ICollection<GlazingMaterial> GlazingMaterials { get; set; } = new List<GlazingMaterial>();

        [DataMember(Order = 10)]
        public ICollection<OpaqueConstruction> OpaqueConstructions { get; set; } = new List<OpaqueConstruction>();

        [DataMember(Order = 1)]
        public ICollection<OpaqueMaterial> OpaqueMaterials { get; set; } = new List<OpaqueMaterial>();

        [DataMember(Order = 10)]
        public ICollection<WindowConstruction> WindowConstructions { get; set; } = new List<WindowConstruction>();

        [DataMember(Order = 10)]
        public ICollection<StructureInformation> StructureDefinitions { get; set; } = new List<StructureInformation>();
        #endregion

        #region Schedules
        [DataMember(Order = 11)]
        public ICollection<DaySchedule> DaySchedules { get; set; } = new List<DaySchedule>();

        [DataMember(Order = 12)]
        public ICollection<WeekSchedule> WeekSchedules { get; set; } = new List<WeekSchedule>();

        [DataMember(Order = 13)]
        public ICollection<YearSchedule> YearSchedules { get; set; } = new List<YearSchedule>();
        #endregion

        #region Zone stuff
        [DataMember(Order = 20)]
        public ICollection<DomesticHotWaterSettings> DomesticHotWaterSettings { get; set; } = new List<DomesticHotWaterSettings>();

        [DataMember(Order = 20)]
        public ICollection<ZoneVentilation> VentilationSettings { get; set; } = new List<ZoneVentilation>();

        [DataMember(Order = 250)]
        public ICollection<WindowSettings> WindowSettings { get; set; } = new List<WindowSettings>();

        [DataMember(Order = 20)]
        public ICollection<ZoneConditioning> ZoneConditionings { get; set; } = new List<ZoneConditioning>();

        [DataMember(Order = 20)]
        public ICollection<ZoneConstructions> ZoneConstructionSets { get; set; } = new List<ZoneConstructions>();

        [DataMember(Order = 20)]
        public ICollection<ZoneLoads> ZoneLoads { get; set; } = new List<ZoneLoads>();

        [DataMember(Order = 30)]
        public ICollection<ZoneDefinition> Zones { get; set; } = new List<ZoneDefinition>();
        #endregion

        public static Library? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Library>(json);
        }

        public static Library? FromXml(string path)
        {
            using (var file = File.OpenRead(path))
            using (var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas()))
            {
                var deserializer = new DataContractSerializer(typeof(Library));
                return (Library?)deserializer.ReadObject(reader);
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
