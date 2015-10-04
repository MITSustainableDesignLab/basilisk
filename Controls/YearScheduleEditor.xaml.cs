using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using PickWeekFunc = System.Func<Basilisk.Controls.InterfaceModels.YearSchedulePart, System.Collections.Generic.IEnumerable<Basilisk.Controls.InterfaceModels.LibraryComponent>, bool>;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for YearScheduleEditor.xaml
    /// </summary>
    public partial class YearScheduleEditor : UserControl
    {
        public YearScheduleEditor()
        {
            InitializeComponent();
        }

        private void BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if ((string)e.Column.Header != "Schedule") { return; }
            var layer = (YearSchedulePart)e.Row.DataContext;
            var cancel = PickWeekSchedule?.Invoke(layer, WeekScheduleChoices);
            e.Cancel = !cancel.HasValue || !cancel.Value;
        }

        private void DeleteSelectedPart(object sender, RoutedEventArgs e)
        {
            var selectedIx = partsGrid.SelectedIndex;
            var layers = Parts;
            if (selectedIx >= 0 && selectedIx < layers.Count)
            {
                layers.RemoveAt(selectedIx);
            }
        }

        private void MoveSelectedPartDown(object sender, RoutedEventArgs e)
        {
            var selectedIx = partsGrid.SelectedIndex;
            var layers = Parts;
            if (selectedIx >= 0 && selectedIx < layers.Count - 1)
            {
                var layer = layers.ElementAt(selectedIx);
                layers.RemoveAt(selectedIx);
                layers.Insert(selectedIx + 1, layer);
                partsGrid.SelectedIndex = selectedIx + 1;
            }
        }

        private void MoveSelectedPartUp(object sender, RoutedEventArgs e)
        {
            var selectedIx = partsGrid.SelectedIndex;
            var parts = Parts;
            if (selectedIx >= 1 && selectedIx < parts.Count)
            {
                var layer = parts.ElementAt(selectedIx);
                parts.RemoveAt(selectedIx);
                parts.Insert(selectedIx - 1, layer);
                partsGrid.SelectedIndex = selectedIx - 1;
            }
        }

        public IEnumerable<LibraryComponent> WeekScheduleChoices
        {
            get { return (IEnumerable<LibraryComponent>)GetValue(WeekScheduleChoicesProperty); }
            set { SetValue(WeekScheduleChoicesProperty, value); }
        }

        public static readonly DependencyProperty WeekScheduleChoicesProperty =
            DependencyProperty.Register(
                nameof(WeekScheduleChoices),
                typeof(IEnumerable<LibraryComponent>),
                typeof(YearScheduleEditor));

        public ObservableCollection<YearSchedulePart> Parts
        {
            get { return (ObservableCollection<YearSchedulePart>)GetValue(PartsProperty); }
            set { SetValue(PartsProperty, value); }
        }

        public static readonly DependencyProperty PartsProperty =
            DependencyProperty.Register(
                nameof(Parts),
                typeof(ObservableCollection<YearSchedulePart>),
                typeof(YearScheduleEditor));

        public PickWeekFunc PickWeekSchedule
        {
            get { return (PickWeekFunc)GetValue(PickWeekScheduleProperty); }
            set { SetValue(PickWeekScheduleProperty, value); }
        }

        public static readonly DependencyProperty PickWeekScheduleProperty =
            DependencyProperty.Register(
                nameof(PickWeekSchedule),
                typeof(PickWeekFunc),
                typeof(YearScheduleEditor));

        public ICollection<SimulationSetting> Settings
        {
            get { return (ICollection<SimulationSetting>)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.Register(
                nameof(Settings),
                typeof(ICollection<SimulationSetting>),
                typeof(YearScheduleEditor));
    }
}
