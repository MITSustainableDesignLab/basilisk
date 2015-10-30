using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    public abstract class WindowMaterialBase : MaterialBase
    {
        protected void CopyBasePropertiesFrom(WindowMaterialBase source)
        {
            CopyBasePropertiesFrom((LibraryComponent)source);
        }
    }
}
