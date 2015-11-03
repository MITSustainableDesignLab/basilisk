using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    [TestClass]
    public class MaterialLayerTests
    {
        [TestMethod]
        public void JsonRoundtrip_Glazing_IsEqual()
        {
            var layer = Dummies.GlazingLayer;
            var roundtripped = JsonSerialization.Roundtrip(layer);
            Assert.AreEqual(layer, roundtripped);
        }

        [TestMethod]
        public void JsonRoundtrip_Gas_IsEqual()
        {
            var layer = Dummies.GasLayer;
            var roundtripped = JsonSerialization.Roundtrip(layer);
            Assert.AreEqual(layer, roundtripped);
        }

        [TestMethod]
        public void JsonRoundtrip_Opaque_IsEqual()
        {
            var layer = Dummies.OpaqueLayer;
            var roundtripped = JsonSerialization.Roundtrip(layer);
            Assert.AreEqual(layer, roundtripped);
        }
    }
}
