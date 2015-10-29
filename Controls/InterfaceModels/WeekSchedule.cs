using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WeekSchedule))]
    [ImmutableCategoryName]
    public class WeekSchedule : LibraryComponent
    {
        public override string Category
        {
            get { return "Week"; }
            set { }
        }

        [SimulationSetting]
        public string Type { get; set; }

        public ObservableCollection<DaySchedule> Days { get; set; }

        public override LibraryComponent Duplicate()
        {
            var res = new WeekSchedule()
            {
                Type = Type
            };
            var days = Days.Select(d => d.Duplicate()).Cast<DaySchedule>();
            res.Days = new ObservableCollection<DaySchedule>(days);
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
