using System.Collections.Generic;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.DaySchedule))]
    [ImmutableCategoryName]
    [DisplayName("day schedule")]
    public class DaySchedule : LibraryComponent
    {
        public DaySchedule()
        {
            Values = Enumerable.Repeat(0.0, 24).ToList();
        }

        public override string Category
        {
            get { return "Day"; }
            set { }
        }

        [SimulationSetting]
        public string Type { get; set; }

        public IList<double> Values { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new DaySchedule()
            {
                Type = Type,
                Values = Values
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
