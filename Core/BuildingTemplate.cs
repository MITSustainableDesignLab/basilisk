﻿using System;
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
        public ZoneDefinition? Core { get; set; }

        [DataMember]
        public int Lifespan { get; set; }

        [DataMember]
        public double PartitionRatio { get; set; }

        [DataMember]
        public ZoneDefinition? Perimeter { get; set; }

        [DataMember]
        public StructureInformation? Structure { get; set; }

        [DataMember]
        public WindowSettings? Windows { get; set; }

        [DataMember]
        public double DefaultWindowToWallRatio { get; set; } = 0.4;

        [DataMember]
        public int? YearFrom { get; set; }

        [DataMember]
        public int? YearTo { get; set; }

        [DataMember]
        public string[] Country { get; set; } = Array.Empty<string>();

        [DataMember]
        public string[] ClimateZone { get; set; } = Array.Empty<string>();

        [DataMember]
        public string[] Authors { get; set; } = Array.Empty<string>();

        [DataMember]
        public string[] AuthorEmails { get; set; } = Array.Empty<string>();

        [DataMember] 
        public string Version { get; set; } = "v1.0";


        internal override IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var core = Core?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var perim = Perimeter?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var structure = Structure?.ReferencedComponents ?? Enumerable.Empty<LibraryComponent>();
                var window = Windows is not null ? Enumerable.Repeat(Windows, 1) : Enumerable.Empty<LibraryComponent>();
                return core.Concat(perim).Concat(structure).Concat(window);
            }
        }
    }
}
