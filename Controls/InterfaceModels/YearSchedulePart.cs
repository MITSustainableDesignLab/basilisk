using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.YearSchedulePart))]
    public class YearSchedulePart
    {
        public WeekSchedule Schedule { get; set; }

        public int FromDay { get; set; }
        public int FromMonth { get; set; }
        public int ToDay { get; set; }
        public int ToMonth { get; set; }
    }
}
