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
        public ZoneDefinition Perimeter { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var core = Core?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var perim = Perimeter?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                return core.Concat(perim);
            }
        }
    }
}
