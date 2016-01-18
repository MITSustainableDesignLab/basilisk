using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [ComponentNamespace]
    public abstract class WindowMaterialBase : MaterialBase
    {
        protected void CopyBasePropertiesFrom(WindowMaterialBase source)
        {
            CopyBasePropertiesFrom((LibraryComponent)source);
        }
    }
}
