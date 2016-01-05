using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Basilisk.Controls.Attributes;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class SimulationSettingsCreator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as LibraryComponent)?.SimulationSettings;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        internal class HierarchyComparer : IComparer<Type>
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
