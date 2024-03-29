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
    public class GasMaterial : WindowMaterialBase, IEquatable<GasMaterial>
    {
        [DataMember]
        public override string Category
        {
            get { return "Gases"; }
            set { /* I'd throw, but then deserialization wouldn't work */ }
        }

        [DataMember, DefaultValue("AIR")]
        public string Type { get; set; } = "AIR";

        internal override IEnumerable<LibraryComponent> ReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        #region Equality
        public bool Equals(GasMaterial? other)
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            else if (Object.ReferenceEquals(other, this)) { return true; }
            return other.Type == this.Type;
        }

        public override bool Equals(object? obj)
        {
            return obj is GasMaterial m && Equals(m);
        }

        public override int GetHashCode()
        {
            // CMR: Not sure the best way to go here (ideally these should be immutable, but...)
            return base.GetHashCode();
        }

        public static bool operator ==(GasMaterial lhs, GasMaterial rhs)
        {
            return (object)lhs == null ? (object)rhs == null : lhs.Equals(rhs);
        }

        public static bool operator !=(GasMaterial lhs, GasMaterial rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
