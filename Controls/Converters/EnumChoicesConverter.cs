using System;
using System.Globalization;
using System.Windows.Data;

namespace Basilisk.Controls.Converters;

public class EnumChoicesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            Enum e => Enum.GetValues(e.GetType()),
            _ => value
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
