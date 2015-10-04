using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.YearSchedule))]
    [ImmutableCategoryName]
    public class YearSchedule : LibraryComponent
    {
        public override string Category
        {
            get { return "Year"; }
            set { }
        }

        public ObservableCollection<YearSchedulePart> Parts { get; set; }
    }
}
