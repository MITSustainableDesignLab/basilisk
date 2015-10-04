using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class UseDefaultValuesOfAttribute : Attribute
    {
        public UseDefaultValuesOfAttribute(Type sourceType)
        {
            SourceType = sourceType;
        }

        public Type SourceType { get; }
    }
}
