using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public class GlazingMaterial : WindowMaterialBase, IEquatable<GlazingMaterial>
    {
        [DataMember, DefaultValue(1.0)]
        public double DirtFactor { get; set; } = 1.0;

        [DataMember, DefaultValue(0.84)]
        public double IREmissivityBack { get; set; } = 0.84;

        [DataMember, DefaultValue(0.84)]
        public double IREmissivityFront { get; set; } = 0.84;

        [DataMember, DefaultValue(0.0)]
        public double IRTransmittance { get; set; }

        [DataMember, DefaultValue(0.075)]
        public double SolarReflectanceBack { get; set; } = 0.075;

        [DataMember, DefaultValue(0.075)]
        public double SolarReflectanceFront { get; set; } = 0.075;

        [DataMember, DefaultValue(0.837)]
        public double SolarTransmittance { get; set; } = 0.837;

        [DataMember, DefaultValue(0.081)]
        public double VisibleReflectanceBack { get; set; } = 0.081;

        [DataMember, DefaultValue(0.081)]
        public double VisibleReflectanceFront { get; set; } = 0.081;

        [DataMember, DefaultValue(0.898)]
        public double VisibleTransmittance { get; set; } = 0.898;

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        #region Equality
        public bool Equals(GlazingMaterial other)
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            else if (Object.ReferenceEquals(other, this)) { return true; }
            return
                MaterialBase.ShareProperties(this, other) &&
                this.DirtFactor == other.DirtFactor &&
                this.IREmissivityBack == other.IREmissivityBack &&
                this.IREmissivityFront == other.IREmissivityFront &&
                this.IRTransmittance == other.IRTransmittance &&
                this.SolarReflectanceBack == other.SolarReflectanceBack &&
                this.SolarReflectanceFront == other.SolarReflectanceFront &&
                this.SolarTransmittance == other.SolarTransmittance &&
                this.VisibleReflectanceBack == other.VisibleReflectanceBack &&
                this.VisibleReflectanceFront == other.VisibleReflectanceFront &&
                this.VisibleTransmittance == other.VisibleTransmittance; 
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as GlazingMaterial);
        }

        public override int GetHashCode()
        {
            // CMR: Not sure the best way to go here (ideally these should be immutable, but...)
            return base.GetHashCode();
        }

        public static bool operator ==(GlazingMaterial lhs, GlazingMaterial rhs)
        {
            return (object)lhs == null ? (object)rhs == null : lhs.Equals(rhs);
        }

        public static bool operator !=(GlazingMaterial lhs, GlazingMaterial rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
