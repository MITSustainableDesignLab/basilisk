using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WindowConstruction))]
    public class WindowConstruction : LayeredConstruction
    {
        public override LibraryComponent Duplicate()
        {
            var res = new WindowConstruction();
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
