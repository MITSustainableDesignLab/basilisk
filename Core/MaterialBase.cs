﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public abstract class MaterialBase : LibraryComponent
    {
        [DataMember]
        public double Conductivity { get; set; }

        [DataMember]
        public double Cost { get; set; }

        [DataMember]
        public double Density { get; set; }

        [DataMember]
        public double EmbodiedCarbon { get; set; }

        [DataMember]
        public double EmbodiedEnergy { get; set; }

        [DataMember]
        [DefaultValue(new double[] { 1.0 })]
        public double[] SubstitutionRatePattern { get; set; } = new[] { 1.0 };

        [DataMember]
        public double SubstitutionTimestep { get; set; }

        [DataMember]
        public double TransportCarbon { get; set; }

        [DataMember]
        public double TransportDistance { get; set; }

        [DataMember]
        public double TransportEnergy { get; set; }

        internal static bool ShareProperties(MaterialBase a, MaterialBase b)
        {
            return
                a.Name == b.Name &&
                a.Conductivity == b.Conductivity &&
                a.Cost == b.Cost &&
                a.Density == b.Density &&
                a.EmbodiedCarbon == b.EmbodiedCarbon &&
                a.EmbodiedEnergy == b.EmbodiedEnergy &&
                a.SubstitutionRatePattern == b.SubstitutionRatePattern &&
                a.SubstitutionTimestep == b.SubstitutionTimestep &&
                a.TransportCarbon == b.TransportCarbon &&
                a.TransportDistance == b.TransportDistance &&
                a.TransportEnergy == b.TransportEnergy;
        }
    }
}
