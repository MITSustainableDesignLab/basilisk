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
        public static MaterialLayer<GasMaterial> GasLayer =>
            new MaterialLayer<GasMaterial>()
            {
                Material = new GasMaterial(),
                Thickness = 0.05
            };

        public static MaterialLayer<GlazingMaterial> GlazingLayer =>
            new MaterialLayer<GlazingMaterial>()
            {
                Material = GlazingMaterial,
                Thickness = 0.1
            };

        public static GlazingMaterial GlazingMaterial =>
            new GlazingMaterial()
            {
                Name = "Test Glazing Material"
            };

        public static OpaqueConstruction OpaqueConstruction
        {
            get
            {
                var c = new OpaqueConstruction() { Name = "Test Opaque Construction" };
                c.Layers.Add(OpaqueLayer);
                c.Layers.Add(OpaqueLayer);
                return c;
            }
        }

        public static MaterialLayer<OpaqueMaterial> OpaqueLayer =>
            new MaterialLayer<OpaqueMaterial>()
            {
                Material = OpaqueMaterial,
                Thickness = 0.3
            };

        public static OpaqueMaterial OpaqueMaterial =>
            new OpaqueMaterial()
            {
                Name = "Test Opaque Material"
            };

        public static WindowConstruction WindowConstruction =>
            new WindowConstruction()
            {
                Name = "Empty Window Construction"
            };

    }
}
