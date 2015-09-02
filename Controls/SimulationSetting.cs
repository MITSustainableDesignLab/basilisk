using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Basilisk.Controls
{
    public class SimulationSetting : INotifyPropertyChanged, IEditableObject
    {
        private readonly object obj;
        private readonly PropertyInfo prop;

        private object backupVal;
        private bool inTxn = false;

        public SimulationSetting(
            object obj,
            PropertyInfo prop,
            string displayName,
            SettingType type = SettingType.Unspecified,
            Type enumType = null)
        {
            PropertyChanged += (s, e) => { };
            this.obj = obj;
            this.prop = prop;

            DisplayName = displayName;

            if (type == SettingType.Unspecified)
            {
                if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                {
                    type = SettingType.Real;
                }
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    type = SettingType.Integer;
                }
                else if (prop.PropertyType == typeof(string))
                {
                    type = SettingType.String;
                }
                else if (prop.PropertyType.IsEnum)
                {
                    type = SettingType.Enum;
                    enumType = prop.PropertyType;
                }
                else
                {
                    throw new ArgumentException("Unknown setting type", "type");
                }
            }
            SettingType = type;
            if (SettingType == SettingType.Enum) { Choices = Enum.GetNames(enumType); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayName { get; private set; }
        public string PropertyName { get { return prop.Name; } }

        public object Value
        {
            get { return prop.GetValue(obj); }
            set
            {
                prop.SetValue(obj, value, BindingFlags.SetProperty, SettingValueBinder.Instance, null, null);
                Update();
            }
        }

        internal string[] Choices { get; private set; }
        internal SettingType SettingType { get; private set; }

        internal Visibility EnumVisibility
        {
            get
            {
                return SettingType == SettingType.Enum ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        internal Visibility IntegerVisibility
        {
            get
            {
                return SettingType == SettingType.Integer ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        internal Visibility RealVisibility
        {
            get
            {
                return SettingType == SettingType.Real ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        internal Visibility StringVisibility
        {
            get
            {
                return SettingType == SettingType.String ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void Update()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }

        #region IEditableObject
        public void BeginEdit()
        {
            if (!inTxn)
            {
                backupVal = Value;
                inTxn = true;
            }
        }

        public void CancelEdit()
        {
            if (inTxn)
            {
                Value = backupVal;
                inTxn = false;
            }
        }

        public void EndEdit()
        {
            if (inTxn)
            {
                backupVal = null;
                inTxn = false;
            }
        }
        #endregion
    }
}
