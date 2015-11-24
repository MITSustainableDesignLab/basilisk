using System;
using System.Reflection;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisplayNameAttribute : Attribute
    {
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
    }

    public static class DisplayNameHelper
    {
        public static string DisplayName(this Type componentType)
        {
            if (!typeof(LibraryComponent).IsAssignableFrom(componentType))
            {
                throw new ArgumentException("Only LibraryComponent subclasses can have display names");
            }
            var attribute = componentType.GetCustomAttribute<DisplayNameAttribute>();
            return attribute?.DisplayName ?? "component";
        }
    }
}
