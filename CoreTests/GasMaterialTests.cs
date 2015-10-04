using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    [TestClass]
    public class GasMaterialTests
    {
        [TestMethod]
        public void JsonSerialize_Valid_DoesNotThrow()
        {
            JsonSerialization.Serialize(new GasMaterial());
        }

        [TestMethod]
        public void XmlSerialize_Valid_DoesNotThrow()
        {
            XmlSerialization.Serialize(new GasMaterial());
        }
    }
}
