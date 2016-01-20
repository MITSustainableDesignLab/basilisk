using System.Collections.Generic;
using System.Runtime.Serialization;

using MaterialBase = ArchsimLib.BaseMaterial;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public abstract class LayeredConstruction<MaterialT> : ConstructionBase
        where MaterialT : MaterialBase
    {
        public abstract IList<MaterialLayer<MaterialT>> Layers { get; set; }
    }
}
