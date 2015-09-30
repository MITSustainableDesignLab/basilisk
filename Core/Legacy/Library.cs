using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Basilisk.Core.Legacy
{
    [Serializable]
    [XmlRoot("LibSerializable", Namespace = "", IsNullable = false)]
    public class Library
    {
        public Library()
        {
            GasMaterials = new List<GasMaterial>();
            GlazingMaterials = new List<GlazingMaterial>();
            OpaqueMaterials = new List<OpaqueMaterial>();
        }

        public DateTime TimeStamp { get; set; }

        [XmlArrayItem("GasMaterial")]
        public List<GasMaterial> GasMaterials { get; set; }

        [XmlArrayItem("GlazingMaterial")]
        public List<GlazingMaterial> GlazingMaterials { get; set; }

        [XmlArrayItem("OpaqueMaterial")]
        public List<OpaqueMaterial> OpaqueMaterials { get; set; }
    }
}
