using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WeekSchedule))]
    [DisplayName("week schedule")]
    [ComponentNamespace]
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

        public ObservableCollection<DaySchedule> Days { get; private set; } = new ObservableCollection<DaySchedule>(Enumerable.Repeat(default(DaySchedule), 7));

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Days
            .Where(day => day != null)
            .Distinct();

        public override bool DirectlyReferences(LibraryComponent component) =>
            Days.Contains(component);

        public override LibraryComponent Duplicate()
        {
            var res = new WeekSchedule()
            {
                Type = Type
            };
            res.Days = new ObservableCollection<DaySchedule>(Days.ToArray());
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
