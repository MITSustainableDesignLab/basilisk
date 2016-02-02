using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.YearSchedule))]
    [DisplayName("year schedule")]
    [ComponentNamespace]
    public class YearSchedule : LibraryComponent
    {
        public override string Category
        {
            get { return "Year"; }
            set { }
        }

        public override bool IsCategoryNameMutable => false;

        public ObservableCollection<YearSchedulePart> Parts { get; set; } = new ObservableCollection<YearSchedulePart>();

        public string Type { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var weeks =
                    Parts
                    .Select(part => part.Schedule)
                    .Where(week => week != null)
                    .Distinct();
                return
                    weeks
                    .Concat(weeks.SelectMany(week => week.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            Parts.Any(part => part.Schedule == component);

        public override LibraryComponent Duplicate()
        {
            var parts =
                Parts
                .Select(part =>
                    new YearSchedulePart()
                    {
                        Schedule = part.Schedule,
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

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (YearSchedule)other;
            Type = c.Type;
            var parts =
                c
                .Parts
                .Select(p => new YearSchedulePart()
                {
                    Schedule = coord.GetWithSameName(p.Schedule),
                    FromDay = p.FromDay,
                    ToDay = p.ToDay,
                    FromMonth = p.FromMonth,
                    ToMonth = p.ToMonth
                });
            Parts = new ObservableCollection<YearSchedulePart>(parts);
            CopyBasePropertiesFrom(c);
        }
    }
}
