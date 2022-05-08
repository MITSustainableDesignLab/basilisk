using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Basilisk.Controls;

internal class BooleanToComboBoxConverter : IValueConverter
{
    public string FalseText { get; set; }

    public string TrueText { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (targetType, value) switch
        {
            (Type t, _) when t == typeof(IEnumerable) => new[] { FalseText, TrueText },
            (Type t, bool v) when t == typeof(string) && !v => FalseText,
            (Type t, bool v) when t == typeof(string) && v => TrueText,
            _ => value
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            string s when s == FalseText => false,
            string s when s == TrueText => true,
            _ => value
        };
    }
}
