using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    public class WindowMaterialBase : MaterialBase
    {
        public override LibraryComponent Duplicate()
        {
            var res = new WindowMaterialBase();
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
