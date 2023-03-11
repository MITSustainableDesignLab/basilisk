using Basilisk.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
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
                GetEnumValuesToDisplay(e),

            (Enum e, Type t) when t == typeof(string) =>
                e.GetType().GetField(value.ToString()).GetCustomAttribute<DisplayTextAttribute>()?.DisplayText ?? value,

            _ => value
        };

        IEnumerable<object> GetEnumValuesToDisplay(Enum e)
        {
            return
                e
                .GetType()
                .GetFields()
                .Where(f => f.Name != "value__") // https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues?view=netframework-4.8
                .Where(f => f.GetCustomAttribute<ObsoleteAttribute>() is null)
                .Select(f => f.GetValue(e));
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
