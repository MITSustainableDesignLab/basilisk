using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.YearSchedule))]
    [ImmutableCategoryName]
    [DisplayName("year schedule")]
    public class YearSchedule : LibraryComponent
    {
        public override string Category
        {
            get { return "Year"; }
            set { }
        }

        public ObservableCollection<YearSchedulePart> Parts { get; set; }

        public string Type { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            Parts.Any(part => part.Schedule == component);

        public override LibraryComponent Duplicate()
        {
            var parts =
                Parts
                .Select(part =>
                    new YearSchedulePart()
                    {
                        Schedule = (WeekSchedule)part.Schedule.Duplicate(),
                        FromDay = part.FromDay,
                        ToDay = part.ToDay,
                        FromMonth = part.FromMonth,
                        ToMonth = part.ToMonth
                    });
            var res = new YearSchedule()
            {
                Parts = new ObservableCollection<YearSchedulePart>(parts)
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
