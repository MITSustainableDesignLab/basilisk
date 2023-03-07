using Basilisk.Core.Attributes;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace Basilisk.Controls.Converters;

public class EnumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value, targetType) switch
        {
            (Enum e, Type t) when t == typeof(IEnumerable) =>
                e.GetType().GetFields().Where(f => f.GetCustomAttribute<ObsoleteAttribute>() is null).Select(f => f.GetValue(e)),

            (Enum e, Type t) when t == typeof(string) =>
                e.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayTextAttribute>()?.DisplayText ?? value,

            _ => value
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
