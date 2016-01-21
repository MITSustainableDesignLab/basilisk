using System;
using System.Collections.Generic;

namespace Basilisk.Controls
{
    public class SimulationSettingsCreator
    {
        internal class HierarchyComparer : IComparer<Type>
        {
            private List<Type> TypeList { get; set; }

            public static HierarchyComparer Build(Type childType)
            {
                var types = new List<Type>();
                while (childType != null)
                {
                    types.Add(childType);
                    childType = childType.BaseType;
                }
                types.Reverse();
                return new HierarchyComparer() { TypeList = types };
            }

            public int Compare(Type x, Type y)
            {
                return TypeList.IndexOf(x).CompareTo(TypeList.IndexOf(y));
            }
        }
    }
}
