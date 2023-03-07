using Basilisk.Core.Attributes;
using System;
using System.Reflection;

namespace Basilisk.Controls.Extensions;

internal static class EnumExtensions
{
    public static T GetCanonicalValue<T>(this T value)
    {
        var enumType = value.GetType();

        var field = enumType.GetField(value.ToString());

        return field.GetCustomAttribute<LegacyChoiceAttribute>() is LegacyChoiceAttribute a
            ? (T)Enum.Parse(enumType, a.ReplaceWith)
            : value;
    }
}
