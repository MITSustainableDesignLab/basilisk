using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    public class SettingsCollection : KeyedCollection<string, SimulationSetting>
    {
        public bool TryGetValue(string propertyName, out SimulationSetting value)
        {
            if (this.Dictionary == null)
            {
                value = null;
                return false;
            }
            return this.Dictionary.TryGetValue(propertyName, out value);
        }

        #region KeyedCollection
        protected override string GetKeyForItem(SimulationSetting item)
        {
            return item.PropertyName;
        }
        #endregion
    }
}
