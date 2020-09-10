using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    internal class SettingValueBinder : Binder
    {
        public static readonly SettingValueBinder Instance = new SettingValueBinder();

        #region Binder
        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] names, out object state)
        {
            throw new NotImplementedException();
        }

        public override object ChangeType(object value, Type type, System.Globalization.CultureInfo culture)
        {
            var inType = value.GetType();
            if (inType == type) { return value; }
            else if (inType == typeof(string))
            {
                var entry = (string)value;
                if (type == typeof(double) || type == typeof(double?))
                {
                    return String.IsNullOrEmpty(entry) ? default(double?) : Double.Parse(entry);
                }
                else if (type == typeof(int) || type == typeof(int?))
                {
                    return String.IsNullOrEmpty(entry) ? default(int?) : Int32.Parse(entry);
                }
                else if (type.IsEnum) { return Enum.Parse(type, entry, ignoreCase: true); }
                else if (type == typeof(double[]))
                {
                    return entry.Split(',').Select(Double.Parse).ToArray();
                }
                else if (type == typeof(string[]))
                {
                    return entry.Split(',').Select(a=>a.Trim()).ToArray();
                }
                else
                {
                    throw new NotSupportedException($"Unsupported simulation setting type '{type}'");
                }
            }
            else
            {
                throw new NotImplementedException("Only string binding is currently supported");
            }
        }

        public override void ReorderArgumentArray(ref object[] args, object state)
        {
            throw new NotImplementedException();
        }

        public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
