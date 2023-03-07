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
                return GetEnumValuesToDisplay(e.GetType());

            case (Enum e, Type t) when t == typeof(string):
                var field = GetCanonicalEnumField(e);

                return field.GetCustomAttribute<DisplayTextAttribute>()?.DisplayText ?? field.GetValue(e);

            case (Enum e, Type t) when t == typeof(object):
                return GetCanonicalEnumField(e).GetValue(e);

            default:
                return value;
        }

        FieldInfo GetCanonicalEnumField(Enum value)
        {
            var enumType = value.GetType();

            var field = enumType.GetField(value.ToString());

            if (field.GetCustomAttribute<LegacyChoiceAttribute>() is LegacyChoiceAttribute a)
            {
                return enumType.GetField(a.ReplaceWith);
            }

            return field;
        }

        IEnumerable<object> GetEnumValuesToDisplay(Type enumType)
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
