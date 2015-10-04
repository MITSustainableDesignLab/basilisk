using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(GasMaterial))]
    [KnownType(typeof(GlazingMaterial))]
    public abstract class WindowMaterialBase : MaterialBase
    {
        // TODO: Add stuff for window calculations
    }
}
