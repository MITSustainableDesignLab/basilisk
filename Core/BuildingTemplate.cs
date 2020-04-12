using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract]
    public class BuildingTemplate : LibraryComponent
    {
        [DataMember]
        public ZoneDefinition Core { get; set; }

        [DataMember]
        public int Lifespan { get; set; }

        [DataMember]
        public double PartitionRatio { get; set; }

        [DataMember]
        public ZoneDefinition Perimeter { get; set; }

        [DataMember]
        public StructureInformation Structure { get; set; }

        [DataMember]
        public WindowSettings Windows { get; set; }

        [DataMember]
        public double DefaultWindowToWallRatio { get; set; } = 0.4;

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var core = Core?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var perim = Perimeter?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var structure = Structure?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var window = Enumerable.Repeat(Windows, 1);
                return core.Concat(perim).Concat(structure).Concat(window);
            }
        }
    }
}
