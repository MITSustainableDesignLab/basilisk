using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for SettingsGrid.xaml
    /// </summary>
    public partial class SettingsGrid : DataGrid
    {
        public SettingsGrid()
        {
            InitializeComponent();
        }

        public IEnumerable<SimulationSetting> Settings
        {
            get { return (IEnumerable<SimulationSetting>)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static readonly DependencyProperty SettingsProperty = DependencyProperty.Register(
            nameof(Settings),
            typeof(IEnumerable<SimulationSetting>),
            typeof(SettingsGrid));

        public string SettingNameColumnHeader
        {
            get => (string)GetValue(SettingNameColumnHeaderProperty);
            set => SetValue(SettingNameColumnHeaderProperty, value);
        }

        public static readonly DependencyProperty SettingNameColumnHeaderProperty = DependencyProperty.Register(
            nameof(SettingNameColumnHeader),
            typeof(string),
            typeof(SettingsGrid),
            new PropertyMetadata("Setting"));

        public string SettingUnitsColumnHeader
        {
            get => (string)GetValue(SettingUnitsColumnHeaderProperty);
            set => SetValue(SettingUnitsColumnHeaderProperty, value);
        }

        public static readonly DependencyProperty SettingUnitsColumnHeaderProperty = DependencyProperty.Register(
            nameof(SettingUnitsColumnHeader),
            typeof(string),
            typeof(SettingsGrid),
            new PropertyMetadata("Units"));

        public string SettingValueColumnHeader
        {
            get => (string)GetValue(SettingValueColumnHeaderProperty);
            set => SetValue(SettingValueColumnHeaderProperty, value);
        }

        public static readonly DependencyProperty SettingValueColumnHeaderProperty = DependencyProperty.Register(
            nameof(SettingValueColumnHeader),
            typeof(string),
            typeof(SettingsGrid),
            new PropertyMetadata("Value"));

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            cb.GetBindingExpression(CheckBox.IsCheckedProperty)?.UpdateSource();
        }

        private void CellGotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            comboBox.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var setting = (SimulationSetting)e.Row.DataContext;
            if (setting.SettingType == SettingType.Bool ||
                setting.SettingType == SettingType.Enum ||
                setting.SettingType == SettingType.Reference)
            {
                e.Cancel = true;
            }
        }
    }
}
