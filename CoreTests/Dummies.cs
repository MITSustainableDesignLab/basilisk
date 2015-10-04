using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    internal static class Dummies
    {
        public static OpaqueConstruction OpaqueConstruction
        {
            get
            {
                var c = new OpaqueConstruction() { Name = "Test Opaque Construction" };
                c.Layers.Add(new MaterialLayer<OpaqueMaterial>() { Material = OpaqueMaterial, Thickness = 0.4 });
                c.Layers.Add(new MaterialLayer<OpaqueMaterial>() { Material = OpaqueMaterial, Thickness = 0.3 });
                return c;
            }
        }

        public static OpaqueMaterial OpaqueMaterial
        {
            get
            {
                return new OpaqueMaterial() { Name= "Test Opaque Material", SpecificHeat = 1.0 };
            }
        }
    }
}
