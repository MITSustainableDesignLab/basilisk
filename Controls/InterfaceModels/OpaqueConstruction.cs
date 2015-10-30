using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.OpaqueConstruction))]
    public class OpaqueConstruction : LayeredConstruction
    {
        public override LibraryComponent Duplicate()
        {
            var res = new OpaqueConstruction();
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
