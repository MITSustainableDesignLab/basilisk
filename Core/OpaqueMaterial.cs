using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class OpaqueMaterial : MaterialBase, IEquatable<OpaqueMaterial>
    {
        [DataMember]
        public double SolarAbsorptance { get; set; }

        [DataMember]
        public double SpecificHeat { get; set; }

        [DataMember]
        public double ThermalEmittance { get; set; }

        [DataMember]
        public double VisibleAbsorptance { get; set; }

        #region Equality
        public bool Equals(OpaqueMaterial other)
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            else if (Object.ReferenceEquals(other, this)) { return true; }
            return
                MaterialBase.ShareProperties(this, other) &&
                this.SolarAbsorptance == other.SolarAbsorptance &&
                this.SpecificHeat == other.SpecificHeat &&
                this.ThermalEmittance == other.ThermalEmittance &&
                this.VisibleAbsorptance == other.VisibleAbsorptance;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as OpaqueMaterial);
        }

        public override int GetHashCode()
        {
            // CMR: Not sure the best way to go here (ideally these should be immutable, but...)
            return base.GetHashCode();
        }

        public static bool operator ==(OpaqueMaterial lhs, OpaqueMaterial rhs)
        {
            return (object)lhs == null ? (object)rhs == null : lhs.Equals(rhs);
        }

        public static bool operator !=(OpaqueMaterial lhs, OpaqueMaterial rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
