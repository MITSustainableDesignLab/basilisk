using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class SimulationSettingsCreator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return Enumerable.Empty<SimulationSetting>(); }
            var sourceType = value.GetType();
            var typeOrderer = HierarchyComparer.Build(sourceType);
            var component = value as LibraryComponent;
            
            return
                sourceType
                .GetProperties()
                .OrderBy(prop => prop.DeclaringType, typeOrderer)
                .Select(prop => new { Prop = prop, Att = prop.GetCustomAttribute<SimulationSettingAttribute>() })
                .Where(x => x.Att != null)
                .Select(x =>
                {
                    var displayName = x.Att.DisplayName == null ? x.Prop.Name : x.Att.DisplayName;
                    var setting = new SimulationSetting(value, x.Prop, displayName);
                    if (component != null)
                    {
                        setting.PropertyChanged += (s, e) => component.RaisePropertyChanged(setting.PropertyName);
                    }
                    return setting;
                })
                .ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private class HierarchyComparer : IComparer<Type>
        {
            private List<Type> TypeList { get; set; }

            public static HierarchyComparer Build(Type childType)
            {
                var types = new List<Type>();
                while (childType != null)
                {
                    types.Add(childType);
                    childType = childType.BaseType;
                }
                types.Reverse();
                return new HierarchyComparer() { TypeList = types };
            }

            public int Compare(Type x, Type y)
            {
                return TypeList.IndexOf(x).CompareTo(TypeList.IndexOf(y));
            }
        }
    }
}
