using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    [TestClass]
    public class BuildingTemplateTests
    {
        [TestMethod]
        public void JsonRountrip_Valid_LifespanSurvives()
        {
            var template = new BuildingTemplate() { Lifespan = 60 };
            var deserialized = JsonSerialization.Roundtrip(template);
            Assert.AreEqual(template.Lifespan, deserialized.Lifespan);
        }
    }
}
