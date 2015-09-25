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
                if (type == typeof(double) || type == typeof(double?))
                {
                    var entry = (string)value;
                    return String.IsNullOrEmpty(entry) ? default(double?) : Double.Parse(entry);
                }
                else if (type == typeof(int) || type == typeof(int?))
                {
                    var entry = (string)value;
                    return String.IsNullOrEmpty(entry) ? default(int?) : Int32.Parse(entry);
                }
                else if (type.IsEnum) { return Enum.Parse(type, (string)value); }
                else
                {
                    throw new NotSupportedException("Simulation settings can only be bound to numeric, string, or enum types");
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
