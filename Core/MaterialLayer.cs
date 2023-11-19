using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract]
    public class MaterialLayer<MaterialT> : IEquatable<MaterialLayer<MaterialT>>
        where MaterialT : MaterialBase
    {
        [DataMember]
        public MaterialT? Material { get; set; }

        [DataMember]
        public double Thickness { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1}m)", Material, Thickness);
        }

        #region Equality
        public bool Equals(MaterialLayer<MaterialT>? other)
        {
            if (other is null)
            {
                return false;
            }
            else if (ReferenceEquals(other, this))
            {
                return true;
            }
            else if (Material is null)
            {
                // This shouldn't ever happen but the nullability checker requires that
                // we handle it.
                return other.Material is null && Thickness == other.Thickness;
            }
            else
            {
                // If == is used, dispatch will be to MaterialBase and not MaterialT,
                // so Equals must be used instead.
                return Material.Equals(other.Material) && Thickness == other.Thickness;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is MaterialLayer<MaterialT> layer && Equals(layer);
        }

        public override int GetHashCode()
        {
            // CMR: Not sure the best way to go here (ideally these should be immutable, but...)
            return base.GetHashCode();
        }

        public static bool operator ==(MaterialLayer<MaterialT> lhs, MaterialLayer<MaterialT> rhs)
        {
            return (object)lhs == null ? (object)rhs == null : lhs.Equals(rhs);
        }

        public static bool operator !=(MaterialLayer<MaterialT> lhs, MaterialLayer<MaterialT> rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
