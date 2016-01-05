using System.Collections.ObjectModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WeekSchedule))]
    [DisplayName("week schedule")]
    public class WeekSchedule : LibraryComponent
    {
        public override string Category
        {
            get { return "Week"; }
            set { }
        }

        public override bool IsCategoryNameMutable => false;

        [SimulationSetting]
        public string Type { get; set; }

        public ObservableCollection<DaySchedule> Days { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            Days.Contains(component);

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
