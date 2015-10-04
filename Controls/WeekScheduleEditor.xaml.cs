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

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for WeekScheduleEditor.xaml
    /// </summary>
    public partial class WeekScheduleEditor : UserControl
    {
        public WeekScheduleEditor()
        {
            InitializeComponent();
        }

        public IEnumerable<DaySchedule> AvailableDaySchedules
        {
            get { return (IEnumerable<DaySchedule>)GetValue(AvailableDaySchedulesProperty); }
            set { SetValue(AvailableDaySchedulesProperty, value); }
        }

        public static readonly DependencyProperty AvailableDaySchedulesProperty =
            DependencyProperty.Register(
                nameof(AvailableDaySchedules),
                typeof(IEnumerable<DaySchedule>),
                typeof(WeekScheduleEditor));

        public ObservableCollection<DaySchedule> Days
        {
            get { return (ObservableCollection<DaySchedule>)GetValue(DaysProperty); }
            set { SetValue(DaysProperty, value); }
        }

        public static readonly DependencyProperty DaysProperty =
            DependencyProperty.Register(
                nameof(Days),
                typeof(ObservableCollection<DaySchedule>),
                typeof(WeekScheduleEditor));

        public ICollection<SimulationSetting> Settings
        {
            get { return (ICollection<SimulationSetting>)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.Register(
                nameof(Settings),
                typeof(ICollection<SimulationSetting>),
                typeof(WeekScheduleEditor));
    }
}
