using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace Basilisk.Controls
{
    public class SimulationSetting : INotifyPropertyChanged, IEditableObject, IDataErrorInfo
    {
        private readonly object obj;
        private readonly PropertyInfo prop;

        private object backupVal;
        private bool inTxn = false;
        private string error = String.Empty;

        public SimulationSetting(
            object obj,
            PropertyInfo prop,
            string displayName,
            SettingType type = SettingType.Unspecified)
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
                else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                {
                    type = SettingType.Bool;
                }
                else if (prop.PropertyType == typeof(string))
                {
                    type = SettingType.String;
                }
                else if (prop.PropertyType.IsEnum)
                {
                    type = SettingType.Enum;
                }
                else if (!prop.PropertyType.IsValueType)
                {
                    type = SettingType.Reference;
                }
                else
                {
                    throw new ArgumentException("Unknown setting type", "type");
                }
            }
            SettingType = type;
            if (SettingType == SettingType.Enum)
            {
                Choices = Enum.GetValues(prop.PropertyType);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IList Choices { get; private set; }
        public string DisplayName { get; private set; }
        public bool ExposeAsCheckbox => SettingType == SettingType.Bool;
        public bool ExposeAsComboBox => SettingType == SettingType.Enum;
        public bool ExposeAsText => !ExposeAsCheckbox && !ExposeAsComboBox;
        public string PropertyName => prop.Name;
        public Type TargetType => prop.PropertyType;

        public object Value
        {
            get
            {
                return prop.GetValue(obj);
            }
            set
            {
                try
                {
                    prop.SetValue(obj, value, BindingFlags.SetProperty, SettingValueBinder.Instance, null, null);
                    Update();
                    error = String.Empty;
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
            }
        }

        internal SettingType SettingType { get; private set; }

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

        #region IDataErrorInfo
        public string Error
        {
            get
            {
                // lol unused by WPF
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                return error;
            }
        }
        #endregion

    }
}
