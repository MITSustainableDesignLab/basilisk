using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Basilisk.Legacy
{
    [Serializable]
    [XmlRoot("LibSerializable", Namespace = "", IsNullable = false)]
    public class Library
    {
        public Library()
        {
            BuildingTemplates = new List<BuildingTemplate>();
            DaySchedules = new List<DaySchedule>();
            GasMaterials = new List<GasMaterial>();
            GlazingMaterials = new List<GlazingMaterial>();
            OpaqueConstructions = new List<OpaqueConstruction>();
            OpaqueMaterials = new List<OpaqueMaterial>();
            WeekSchedules = new List<WeekSchedule>();
            YearSchedules = new List<YearSchedule>();
        }

        public DateTime TimeStamp { get; set; }

        [XmlArrayItem("BuildingTemplate")]
        public List<BuildingTemplate> BuildingTemplates { get; set; }

        [XmlArrayItem("DaySchedule")]
        public List<DaySchedule> DaySchedules { get; set; }

        [XmlArrayItem("GasMaterial")]
        public List<GasMaterial> GasMaterials { get; set; }

        [XmlArrayItem("GlazingConstruction")]
        public List<GlazingConstruction> GlazingConstructions { get; set; }

        [XmlArrayItem("GlazingMaterial")]
        public List<GlazingMaterial> GlazingMaterials { get; set; }

        [XmlArrayItem("OpaqueConstruction")]
        public List<OpaqueConstruction> OpaqueConstructions { get; set; }

        [XmlArrayItem("OpaqueMaterial")]
        public List<OpaqueMaterial> OpaqueMaterials { get; set; }

        [XmlArrayItem("StructureType")]
        public List<StructureType> StructureTypes { get; set; }

        [XmlArrayItem("WeekSchedule")]
        public List<WeekSchedule> WeekSchedules { get; set; }

        [XmlArrayItem("YearSchedule")]
        public List<YearSchedule> YearSchedules { get; set; }
    }
}
