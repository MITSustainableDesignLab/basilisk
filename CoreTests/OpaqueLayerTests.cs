using System;
using System.IO;
using System.Runtime.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace CoreTests
{
    [TestClass]
    public class OpaqueLayerTests
    {
        [TestMethod]
        public void JsonRoundtrip_Valid_Matches()
        {
            var mat = Dummies.OpaqueMaterial;
            var serialized = new MaterialLayer<OpaqueMaterial>()
            {
                Material = mat,
                Thickness = 0.5
            };
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.AreEqual(serialized.Material, deserialized.Material);
            Assert.AreEqual(serialized.Thickness, deserialized.Thickness);
        }

        [TestMethod]
        public void Equality_StructurallyEqual_AreEqual()
        {
            var layer1 = new MaterialLayer<OpaqueMaterial>()
            {
                Material = Dummies.OpaqueMaterial,
                Thickness = 0.3
            };
            var layer2 = new MaterialLayer<OpaqueMaterial>()
            {
                Material = Dummies.OpaqueMaterial,
                Thickness = 0.3
            };
            Assert.AreEqual(layer1, layer2);
        }
    }
}
