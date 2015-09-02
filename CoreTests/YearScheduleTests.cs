using System;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Core;

namespace CoreTests
{
    [TestClass]
    public class YearScheduleTests
    {
        [TestMethod]
        public void JsonSerialize_DuplicateWeeks_SingleEntity()
        {
            var days =
                Enumerable
                .Range(0, 7)
                .Select(_ => new DaySchedule() { Name = "Day", Values = Enumerable.Repeat(0.5, 24).ToArray() })
                .ToArray();
            var week = new WeekSchedule() { Name = "Test Week Schedule", Days = days };
            var part1 = new YearSchedulePart() { Schedule = week };
            var part2 = new YearSchedulePart() { Schedule = week };
            var parts = new YearSchedulePart[] { part1, part2 };
            var year = new YearSchedule() { Name = "Test Year Schedule", Parts = parts };
            var json = JsonSerialization.Serialize(year);
            var m = Regex.Matches(json, week.Name);
            Assert.AreEqual(1, m.Count);
        }
    }
}
