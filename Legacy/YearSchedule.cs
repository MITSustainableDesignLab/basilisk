using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public class YearSchedule
    {
        public string Comments { get; set; }
        public string DataSource { get; set; }
        public List<int> DayFrom { get; set; }
        public List<int> DayTill { get; set; }
        public List<int> MonthFrom { get; set; }
        public List<int> MonthTill { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> WeekScheduleNames { get; set; }
    }
}
