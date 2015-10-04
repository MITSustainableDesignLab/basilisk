using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void Creation_DefaultConstructorOnly_NonNullTemplateList()
        {
            var lib = new Library();
            Assert.AreNotEqual(null, lib.BuildingTemplates);
        }

        [TestMethod]
        public void JsonRoundtrip_Valid_OpaqueMaterialsMatch()
        {
            var opaqueMats = new List<OpaqueMaterial>() { Dummies.OpaqueMaterial };
            var serialized = new Library()
            {
                OpaqueMaterials = opaqueMats
            };
            var deserialized = JsonSerialization.Roundtrip(serialized);
            Assert.AreEqual(serialized.OpaqueMaterials.Count, deserialized.OpaqueMaterials.Count);
            foreach (var m in serialized.OpaqueMaterials)
            {
                Assert.IsTrue(deserialized.OpaqueMaterials.Contains(m));
            }
        }
    }
}
