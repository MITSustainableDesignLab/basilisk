using Basilisk.Core.Attributes;
using System;
using System.Collections;
using System.Globalization;
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
                Enum.GetValues(e.GetType()),

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
