using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    [TestClass]
    public class WindowMaterialBaseTests
    {
        [TestMethod]
        public void JsonRoundtrip_GasAsAbstractBaseClass_CorrectlyDeserializes()
        {
            WindowMaterialBase serialized = new GasMaterial() { Name = "Test Gas" };
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.IsInstanceOfType(deserialized, typeof(GasMaterial));
        }

        [TestMethod]
        public void JsonRoundtrip_GlazingAsAbstractBaseClass_CorrectlyDeserializes()
        {
            WindowMaterialBase serialized = new GlazingMaterial() { Name = "Test Glazing" };
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.IsInstanceOfType(deserialized, typeof(GlazingMaterial));
        }

        [TestMethod]
        public void XmlRoundtrip_GasAsAbstractBaseClass_CorrectlyDeserializes()
        {
            WindowMaterialBase serialized = new GasMaterial() { Name = "Test Gas" };
            var deserialized = XmlSerialization.Roundtrip(serialized);
            Assert.IsInstanceOfType(deserialized, typeof(GasMaterial));
        }

        [TestMethod]
        public void XmlRoundtrip_GlazingAsAbstractBaseClass_CorrectlyDeserializes()
        {
            WindowMaterialBase serialized = new GlazingMaterial() { Name = "Test Glazing" };
            var deserialized = XmlSerialization.Roundtrip(serialized);
            Assert.IsInstanceOfType(deserialized, typeof(GlazingMaterial));
        }
    }
}
