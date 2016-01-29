using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class SimulationSetting : INotifyPropertyChanged, IEditableObject, IDataErrorInfo
    {
        private readonly IReadOnlyList<LibraryComponent> components;
        private readonly PropertyInfo prop;
        private readonly ComponentCoordinator coordinator;

        private IReadOnlyList<object> backupVals;
        private bool inTxn = false;
        private string error = String.Empty;

        public SimulationSetting(
            LibraryComponent component,
            PropertyInfo prop,
            string displayName,
            ComponentCoordinator coordinator,
            SettingType type = SettingType.Unspecified)
        {
            var multiple = component as LibraryComponentSet;
            components = multiple == null ? new List<LibraryComponent>() { component } : multiple.Components.ToList();
            this.prop = prop;
            this.coordinator = coordinator;

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
                    throw new ArgumentException("Unknown setting type", nameof(type));
                }
            }
            SettingType = type;
            if (SettingType == SettingType.Enum)
            {
                EnumChoices = Enum.GetValues(prop.PropertyType);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IList EnumChoices { get; private set; }
        public string DisplayName { get; private set; }
        public string PropertyName => prop.Name;
        public IEnumerable<LibraryComponent> ReferenceChoices => coordinator.ComponentsOfType(prop.PropertyType);
        public bool ShowMultivalueDescription => MultipleValueDescriptionText != null;
        public Type TargetType => prop.PropertyType;

        public object SingleValue
        {
            get
            {
                var distinct = components.Select(prop.GetValue).Distinct();
                if (prop.PropertyType == typeof(bool))
                {
                    return
                        distinct.Count() == 0 ? false :
                        distinct.Count() == 1 ? distinct.First() :
                        default(bool?);
                }
                return distinct.Count() == 1 ? distinct.Single() : null;
            }
            set
            {
                try
                {
                    foreach (var c in components)
                    {
                        prop.SetValue(c, value, BindingFlags.SetProperty, SettingValueBinder.Instance, null, null);
                    }
                    Update();
                    error = String.Empty;
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
            }
        }

        public string MultipleValueDescriptionText
        {
            get
            {
                if (components.Count <= 1 || prop.PropertyType == typeof(bool)) { return null; }
                else if (prop.PropertyType == typeof(string))
                {
                    var vals =
                        components
                        .Select(prop.GetValue)
                        .Cast<string>()
                        .Distinct()
                        .Where(v => !String.IsNullOrEmpty(v))
                        .ToArray();
                    if (vals.Length <= 1) { return null; }
                    var joined = String.Join(", ", vals);
                    return $"Multiple values: {joined}";
                }
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                {
                    var vals =
                        components
                        .Select(c => Convert.ToDouble(prop.GetValue(c)))
                        .Cast<double?>()
                        .ToArray();
                    return $"Values range from {vals.Min()} to {vals.Max()}";
                }
                else if (!prop.PropertyType.IsValueType)
                {
                    var vals =
                        components
                        .Select(prop.GetValue)
                        .Cast<LibraryComponent>()
                        .Distinct()
                        .Select(c => c.Name)
                        .ToArray();
                    if (vals.Length <= 1) { return null; }
                    var joined = String.Join(", ", vals);
                    return $"Multiple values: {joined}";
                }
                return null;
            }
        }

        internal SettingType SettingType { get; private set; }

        public void Update()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SingleValue)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MultipleValueDescriptionText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowMultivalueDescription)));
        }

        #region IEditableObject
        public void BeginEdit()
        {
            if (!inTxn)
            {
                backupVals = components.Select(prop.GetValue).ToList();
                inTxn = true;
            }
        }

        public void CancelEdit()
        {
            if (inTxn)
            {
                foreach (var x in components.Zip(backupVals, (c, v) => new { C = c, V = v }))
                {
                    prop.SetValue(x.C, x.V, BindingFlags.SetProperty, SettingValueBinder.Instance, null, null);
                }
                error = String.Empty;
                Update();
                inTxn = false;
            }
        }

        public void EndEdit()
        {
            if (inTxn)
            {
                backupVals = null;
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
