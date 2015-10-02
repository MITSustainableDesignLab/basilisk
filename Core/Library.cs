using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract]
    public class Library
    {
        public Library()
        {
            Templates = new List<BuildingTemplate>();
            GasMaterials = new List<GasMaterial>();
            GlazingMaterials = new List<GlazingMaterial>();
            OpaqueConstructions = new List<OpaqueConstruction>();
            OpaqueMaterials = new List<OpaqueMaterial>();
            WindowConstructions = new List<WindowConstruction>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<BuildingTemplate> Templates { get; set; }

        #region Constructions and materials
        public IEnumerable<WindowMaterialBase> AllWindowMaterials { get { return GlazingMaterials.Cast<WindowMaterialBase>().Concat(GasMaterials); } }

        [DataMember]
        public ICollection<GasMaterial> GasMaterials { get; set; }

        [DataMember]
        public ICollection<GlazingMaterial> GlazingMaterials { get; set; }

        [DataMember]
        public ICollection<OpaqueConstruction> OpaqueConstructions { get; set; }

        [DataMember]
        public ICollection<OpaqueMaterial> OpaqueMaterials { get; set; }

        [DataMember]
        public ICollection<WindowConstruction> WindowConstructions { get; set; }
        #endregion
    }
}
