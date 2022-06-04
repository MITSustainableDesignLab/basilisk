using Basilisk.Controls.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Basilisk.Controls.Extensions;

public static class TypeExtensions
{
    public static object CreateComponentWithDefaults(this Type type)
    {
        var newComponent = Activator.CreateInstance(type);

        var localDefaults = GetDefaults(type);
        var sourceDefaults = GetDefaults(type.GetCustomAttribute<UseDefaultValuesOfAttribute>()?.SourceType);

        foreach (var prop in type.GetProperties())
        {
            if (localDefaults.TryGetValue(prop.Name, out var def) ||
                sourceDefaults.TryGetValue(prop.Name, out def))
            {
                prop.SetValue(newComponent, def);
            }
        }

        return newComponent;

        static IEnumerable<DefaultValue> GetDefaultValues(Type t)
        {
            if (t is null)
            {
                yield break;
            }

            foreach (var property in t.GetProperties())
            {
                if (property.GetCustomAttribute<DefaultValueAttribute>() is DefaultValueAttribute a)
                {
                    yield return new DefaultValue(property.Name, a.Value);
                }
                else if (property.PropertyType.GetCustomAttribute<UseDefaultValuesOfAttribute>() is UseDefaultValuesOfAttribute source)
                {
                    yield return new DefaultValue(property.Name, property.PropertyType.CreateComponentWithDefaults());
                }
            }
        }

        static Dictionary<string, object> GetDefaults(Type t) =>
            GetDefaultValues(t).ToDictionary(dv => dv.PropertyName, dv => dv.Value);
    }

    private class DefaultValue
    {
        public DefaultValue(string propertyName, object value)
        {
            Value = value;
            PropertyName = propertyName;
        }

        public object Value { get; }

        public string PropertyName { get; }
    }
}
