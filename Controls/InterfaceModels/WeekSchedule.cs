using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

using Basilisk.Controls.Attributes;
using System;

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
        [DefaultValue("Fraction")]
        public string Type { get; set; } = "Fraction";

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

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (WeekSchedule)other;
            Type = c.Type;
            var days =
                c
                .Days
                .Select(coord.GetWithSameName)
                .Cast<DaySchedule>();
            Days = new ObservableCollection<DaySchedule>(days);
            CopyBasePropertiesFrom(c);
        }
    }
}
