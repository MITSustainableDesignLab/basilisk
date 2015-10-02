using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace CoreTests
{
    [TestClass]
    public class OpaqueMaterialTests
    {
        [TestMethod]
        public void JsonRoundtrip_Valid_Matches()
        {
            var serialized = Dummies.OpaqueMaterial;
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.IsTrue(serialized.Equals(deserialized));
        }

        [TestMethod]
        public void JsonSerialize_TwoReferences_SingleEntity()
        {
            var mat = Dummies.OpaqueMaterial;
            var layer1 = new MaterialLayer<OpaqueMaterial>() { Material = mat, Thickness = 0.3 };
            var layer2 = new MaterialLayer<OpaqueMaterial>() { Material = mat, Thickness = 0.4 };
            var layers = new MaterialLayer<OpaqueMaterial>[]
            { 
                new MaterialLayer<OpaqueMaterial>() { Material = mat, Thickness = 0.3 },
                new MaterialLayer<OpaqueMaterial>() { Material = mat, Thickness = 0.4 }
            };
            var c = new OpaqueConstruction()
            {
                Name = "Test Construction",
                Layers = layers
            };
            var json = JsonSerialization.Serialize(c);
            var m = Regex.Matches(json, mat.Name);
            Assert.AreEqual(1, m.Count);
        }

        [TestMethod]
        public void JsonRoundtrip_TwoReferences_SingleEntity()
        {
            var mat = Dummies.OpaqueMaterial;
            var layer1 = new MaterialLayer<OpaqueMaterial>() { Material = mat, Thickness = 0.3 };
            var layer2 = new MaterialLayer<OpaqueMaterial>() { Material = mat, Thickness = 0.4 };
            var layers = new MaterialLayer<OpaqueMaterial>[] { layer1, layer2 };
            var serialized = new OpaqueConstruction() { Name = "Test Construction", Layers = layers };
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.IsTrue(Object.ReferenceEquals(deserialized.Layers[0].Material, deserialized.Layers[1].Material));
        }
    }
}
