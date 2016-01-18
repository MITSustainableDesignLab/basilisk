using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WindowConstruction))]
    [DisplayName("window construction")]
    [ComponentNamespace]
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
