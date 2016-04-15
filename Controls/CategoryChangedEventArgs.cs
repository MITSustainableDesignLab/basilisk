using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    public class CategoryChangedEventArgs : EventArgs
    {
        public CategoryChangedEventArgs(ComponentCategory category)
        {
            Category = category;
        }

        public ComponentCategory Category { get; }
    }
}
