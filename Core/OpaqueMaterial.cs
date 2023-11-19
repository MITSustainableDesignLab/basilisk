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
        public double? DesignStrength { get; set; }

        [DataMember]
        public double? ModulusOfElasticity { get; set; }

        [DataMember]
        public double MoistureDiffusionResistance { get; set; }

        [DataMember]
        public string? Roughness { get; set; }

        [DataMember]
        public double SolarAbsorptance { get; set; }

        [DataMember]
        public double SpecificHeat { get; set; }

        [DataMember]
        public double ThermalEmittance { get; set; }

        [DataMember]
        public double VisibleAbsorptance { get; set; }

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        #region Equality
        public bool Equals(OpaqueMaterial? other)
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            else if (Object.ReferenceEquals(other, this)) { return true; }
            return
                MaterialBase.ShareProperties(this, other) &&
                this.DesignStrength == other.DesignStrength &&
                this.ModulusOfElasticity == other.ModulusOfElasticity &&
                this.MoistureDiffusionResistance == other.MoistureDiffusionResistance &&
                this.SolarAbsorptance == other.SolarAbsorptance &&
                this.SpecificHeat == other.SpecificHeat &&
                this.ThermalEmittance == other.ThermalEmittance &&
                this.VisibleAbsorptance == other.VisibleAbsorptance;
        }

        public override bool Equals(object? obj)
        {
            return obj is OpaqueMaterial m && Equals(m);
        }

        public override int GetHashCode()
        {
            // TODO: omg this is super busted
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
