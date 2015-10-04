using System;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace Basilisk.Tests.Core
{
    [TestClass]
    public class WeekScheduleTests
    {
        [TestMethod]
        public void JsonRoundTrip_TypicalData_DayValuesMatch()
        {
            var days =
                Enumerable
                .Range(0, 7)
                .Select(_ => new DaySchedule() { Name = "Day", Values = Enumerable.Repeat(0.5, 24).ToArray() })
                .ToArray();
            var week = new WeekSchedule() { Name = "Week", Days = days };
            var deserialized = JsonSerialization.Roundtrip(week);
            Assert.AreEqual(week.Days.Length, deserialized.Days.Length);
            foreach (var x in week.Days.Zip(deserialized.Days, (s, d) => new { S = s, D = d }))
            {
                Assert.IsTrue(DaySchedule.ValuesMatch(x.S, x.D));
            }
        }

        [TestMethod]
        public void JsonSerialize_DuplicateDays_SingleEntity()
        {
            var hours = Enumerable.Repeat(0.8, 24).ToArray();
            var day = new DaySchedule() { Name = "Test Day Schedule", Values = hours };
            var week = new WeekSchedule()
            {
                Name = "Test Week Schedule",
                Days = Enumerable.Repeat(day, 7).ToArray()
            };
            var json = JsonSerialization.Serialize(week);
            var m = Regex.Matches(json, day.Name);
            Assert.AreEqual(1, m.Count);
        }
    }
}
