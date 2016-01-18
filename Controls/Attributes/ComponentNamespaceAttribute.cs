using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Basilisk.Controls.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentNamespaceAttribute : Attribute
    {
        public static IEnumerable<Type> NamespaceTypes(Type t)
        {
            while (t != null)
            {
                if (t.GetCustomAttribute<ComponentNamespaceAttribute>() != null)
                {
                    yield return t;
                }
                t = t.BaseType;
            }
        }
    }
}
