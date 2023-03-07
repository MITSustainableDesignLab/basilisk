using System;

namespace Basilisk.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class LegacyChoiceAttribute : Attribute
{
    public LegacyChoiceAttribute(string replaceWith)
    {
        ReplaceWith = replaceWith;
    }

    public string ReplaceWith { get; }
}
