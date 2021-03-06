﻿using System;
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
