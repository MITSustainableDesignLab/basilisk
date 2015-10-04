using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Legacy
{
    public class DaySchedule
    {
        public string Comments { get; set; }
        public string DataSource { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<double> Values { get; set; }
    }
}
