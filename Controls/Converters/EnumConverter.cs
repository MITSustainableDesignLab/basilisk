using Basilisk.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Basilisk.Controls.Converters;

public class EnumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value, targetType)
        {
            case (Enum e, Type t) when t == typeof(IEnumerable):
                return EnumValuesToDisplay(e.GetType());

            case (Enum e, Type t) when t == typeof(string):
                var enumType = e.GetType();

                var field = enumType.GetField(value.ToString());

                if (field.GetCustomAttribute<LegacyChoiceAttribute>() is LegacyChoiceAttribute a)
                {
                    field = enumType.GetField(a.ReplaceWith);
                }

                return field.GetCustomAttribute<DisplayTextAttribute>()?.DisplayText ?? field.GetValue(e);

            default:
                return value;
        }

        IEnumerable<object> EnumValuesToDisplay(Type enumType)
        {
            foreach (var value in Enum.GetValues(enumType))
            {
                var name = Enum.GetName(enumType, value);

                var field = enumType.GetField(name);

                if (field.GetCustomAttribute<LegacyChoiceAttribute>() is null)
                {
                    yield return value;
                }
            }
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
