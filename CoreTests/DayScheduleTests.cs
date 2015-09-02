using System;
using System.Collections;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace CoreTests
{
    [TestClass]
    public class DayScheduleTests
    {
        [TestMethod]
        public void JsonRoundtrip_TypicalData_Matches()
        {
            var schedule = new DaySchedule() { Name = "Test Day Schedule" };
            schedule.Values = Enumerable.Repeat(0.5, 24).ToArray();
            var deserialized = JsonSerialization.Roundtrip(schedule);
            Assert.AreEqual(schedule.Name, deserialized.Name);
            Assert.AreEqual(schedule.Type, deserialized.Type);
            Assert.IsTrue(DaySchedule.ValuesMatch(schedule, deserialized));
        }

        [TestMethod]
        public void ValuesMatch_EqualData_True()
        {
            var a = new DaySchedule() { Name = "A", Values = Enumerable.Repeat(0.5, 24).ToArray() };
            var b = new DaySchedule() { Name = "B", Values = Enumerable.Repeat(0.5, 24).ToArray() };
            Assert.IsTrue(DaySchedule.ValuesMatch(a, b));
        }

        [TestMethod]
        public void ValuesMatch_BothNull_True()
        {
            Assert.IsTrue(DaySchedule.ValuesMatch(
                new DaySchedule() { Name = "A" },
                new DaySchedule() { Name = "B" }));
        }

        [TestMethod]
        public void ValuesMatch_OneNull_False()
        {
            var a = new DaySchedule() { Name = "Not Null", Values = Enumerable.Repeat(0.5, 24).ToArray() };
            var b = new DaySchedule() { Name = "Null", Values = null };
            Assert.IsFalse(DaySchedule.ValuesMatch(a, b));
        }

        [TestMethod]
        public void ValuesMatch_DifferentData_False()
        {
            var a = new DaySchedule() { Name = "A", Values = Enumerable.Repeat(0.5, 24).ToArray() };
            var b = new DaySchedule() { Name = "B", Values = Enumerable.Repeat(0.8, 24).ToArray() };
            Assert.IsFalse(DaySchedule.ValuesMatch(a, b));
        }
    }
}
