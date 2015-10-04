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
    }
}
