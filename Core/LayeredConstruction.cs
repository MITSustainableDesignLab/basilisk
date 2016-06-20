using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract(IsReference = true)]
    public abstract class LayeredConstruction<MaterialT> : ConstructionBase
        where MaterialT : MaterialBase
    {
        public abstract IList<MaterialLayer<MaterialT>> Layers { get; set; }
    }
}
