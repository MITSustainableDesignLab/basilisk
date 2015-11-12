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

        private void BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var setting = (SimulationSetting)e.Row.DataContext;
            if (setting.SettingType != SettingType.Reference) { return; }
            var success = PickReferencedComponent?.Invoke(setting);
            if (success.HasValue && success.Value)
            {
                ((DataGrid)sender).CommitEdit();
            }
            e.Cancel = true;
        }

        public Func<SimulationSetting, bool> PickReferencedComponent
        {
            get { return (Func<SimulationSetting, bool>)GetValue(PickReferencedComponentProperty); }
            set { SetValue(PickReferencedComponentProperty, value); }
        }

        public IEnumerable<SimulationSetting> Settings
        {
            get { return (IEnumerable<SimulationSetting>)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static readonly DependencyProperty PickReferencedComponentProperty = DependencyProperty.Register(
            nameof(PickReferencedComponent),
            typeof(Func<SimulationSetting, bool>),
            typeof(SettingsGrid));

        public static readonly DependencyProperty SettingsProperty = DependencyProperty.Register(
            nameof(Settings),
            typeof(IEnumerable<SimulationSetting>),
            typeof(SettingsGrid));

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
    }
}
