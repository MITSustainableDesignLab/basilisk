using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace CoreTests
{
    [TestClass]
    public class OpaqueConstructionTests
    {
        [TestMethod]
        public void JsonRoundtrip_Valid_Matches()
        {
            var serialized = Dummies.OpaqueConstruction;
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.AreEqual(serialized.Name, deserialized.Name);
            foreach (var x in serialized.Layers.Zip(deserialized.Layers, (s, d) => new { S = s, D = d }))
            {
                Assert.AreEqual(x.S.Material, x.D.Material);
                Assert.AreEqual(x.S.Thickness, x.D.Thickness);
            }
        }
    }
}
