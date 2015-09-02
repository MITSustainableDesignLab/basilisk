using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    public static class Util
    {
        public static ValueT GetDefaultValue<ObjectT, ValueT>(Expression<Func<ObjectT, ValueT>> property)
        {
            var body = property.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Default value retrieval requires an expression tree consisting of a single property expression", "property");
            }
            var prop = body.Member as PropertyInfo;
            if (prop == null)
            {
                throw new ArgumentException("Default value retrieval requires an expression tree consisting of a single property expression", "property");
            }
            var defaultAtt = prop.GetCustomAttribute<DefaultValueAttribute>();
            return defaultAtt == null ? default(ValueT) : (ValueT)defaultAtt.Value;
        }
    }
}
