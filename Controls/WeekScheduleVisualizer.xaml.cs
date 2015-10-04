using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for WeekScheduleVisualizer.xaml
    /// </summary>
    public partial class WeekScheduleVisualizer : UserControl
    {
        private const string SeriesName = "Hourly Values";

        public WeekScheduleVisualizer()
        {
            InitializeComponent();
            var area = chart.ChartAreas[0];
            area.AxisX.LabelStyle.Enabled = false;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.LabelStyle.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
        }

        private void OnDaysChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Redraw((IEnumerable<DaySchedule>)sender);
        }

        private void OnNewDaysCollection(ObservableCollection<DaySchedule> oldVal, ObservableCollection<DaySchedule> newVal)
        {
            if (oldVal != null) { oldVal.CollectionChanged -= OnDaysChanged; }
            if (newVal != null) { newVal.CollectionChanged += OnDaysChanged; }
            Redraw(newVal);
        }

        private void Redraw(IEnumerable<DaySchedule> days)
        {
            if (days == null)
            {
                chart.Series.RemoveAt(0);
                chart.Invalidate();
                return;
            }
            var newSeries = new Series(SeriesName)
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Black
            };
            var points =
                days
                .SelectMany(day => day.Values)
                .Select((y, i) => new DataPoint(i + 1, y));
            foreach (var dp in points) { newSeries.Points.Add(dp); }
            chart.Series[SeriesName] = newSeries;
            chart.Invalidate();
        }

        public ObservableCollection<DaySchedule> Days
        {
            get { return (ObservableCollection<DaySchedule>)GetValue(DaysProperty); }
            set { SetValue(DaysProperty, value); }
        }

        public static readonly DependencyProperty DaysProperty =
            DependencyProperty.Register(
                nameof(Days),
                typeof(ObservableCollection<DaySchedule>),
                typeof(WeekScheduleVisualizer),
                new FrameworkPropertyMetadata((s, e) =>
                    ((WeekScheduleVisualizer)s).OnNewDaysCollection((ObservableCollection<DaySchedule>)e.OldValue, (ObservableCollection<DaySchedule>)e.NewValue)));
    }
}
