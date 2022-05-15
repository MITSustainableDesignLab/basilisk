using System;

namespace Basilisk.Core.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class DisplayTextAttribute : Attribute
{
    public DisplayTextAttribute(string text)
    {
        DisplayText = text;
    }

    public string DisplayText { get; }
}
