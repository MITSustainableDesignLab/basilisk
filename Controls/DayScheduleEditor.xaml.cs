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

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for DayScheduleEditor.xaml
    /// </summary>
    public partial class DayScheduleEditor : UserControl
    {
        public DayScheduleEditor()
        {
            InitializeComponent();
        }

        public ICollection<SimulationSetting> Settings
        {
            get { return (ICollection<SimulationSetting>)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.Register(
                nameof(Settings),
                typeof(ICollection<SimulationSetting>),
                typeof(DayScheduleEditor));

        public IList<double> Values
        {
            get { return (IList<double>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register(
                nameof(Values),
                typeof(IList<double>),
                typeof(DayScheduleEditor),
                new FrameworkPropertyMetadata(
                    Enumerable.Repeat(0.0, 24).ToArray(),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
