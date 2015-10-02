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
            GasMaterials = new List<GasMaterial>();
            GlazingMaterials = new List<GlazingMaterial>();
            OpaqueConstructions = new List<OpaqueConstruction>();
            OpaqueMaterials = new List<OpaqueMaterial>();
        }

        public DateTime TimeStamp { get; set; }

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
    }
}
