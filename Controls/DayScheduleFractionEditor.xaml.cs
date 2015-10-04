using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for DayScheduleFractionEditor.xaml
    /// </summary>
    public partial class DayScheduleFractionEditor : UserControl
    {
        private const string SeriesName = "Hourly Values";

        private DataPoint pointCurrentlyBeingChanged = null;

        public DayScheduleFractionEditor()
        {
            InitializeComponent();
            var chartArea = chart.ChartAreas[0];
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 6F);
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = false;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.MajorTickMark.Enabled = false;
            chartArea.AxisY.LabelStyle.Enabled = false;
            chartArea.AxisY.Minimum = 0.0;
            chartArea.AxisY.Maximum = 1.05;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorTickMark.Enabled = false;
        }

        protected void OnValuesChanged(IList<double> newValues)
        {
            if (newValues == null)
            {
                host.Visibility = Visibility.Hidden;
                return;
            }
            var newSeries = new Series(SeriesName)
            {
                ChartType = SeriesChartType.Column,
                Color = System.Drawing.Color.Black,
                Font = new Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0),
                IsValueShownAsLabel = true
            };
            var dps = newValues.Select((y, i) => new DataPoint(i + 1, y));
            foreach (var dp in dps) { newSeries.Points.Add(dp); }
            chart.Series[SeriesName] = newSeries;
            host.Visibility = Visibility.Visible;
            chart.Invalidate();
        }

        public IList<double> Values
        {
            get { return (IList<double>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register(
                nameof(Values),
                typeof(IList<double>),
                typeof(DayScheduleFractionEditor),
                new FrameworkPropertyMetadata(
                    Enumerable.Repeat(0.0, 24).ToArray(),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    (s, e) => ((DayScheduleFractionEditor)s).OnValuesChanged((IList<double>)e.NewValue)));

        private void chart_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (pointCurrentlyBeingChanged != null)
            {
                var yCoord = Math.Min(Math.Max(e.Y, 0), chart.Size.Height - 1);
                var yVal = chart.ChartAreas[0].AxisY.PixelPositionToValue(yCoord);
                yVal = Math.Min(Math.Max(yVal, 0.0), 1.0);
                pointCurrentlyBeingChanged.YValues[0] = Math.Round(yVal, 1);
                chart.Invalidate();
            }
            else
            {
                var hit = chart.HitTest(e.X, e.Y);
                if (hit.ChartElementType == ChartElementType.DataPoint ||
                    hit.ChartElementType == ChartElementType.DataPointLabel)
                {
                    chart.Cursor = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    chart.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
        }

        private void chart_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var hit = chart.HitTest(e.X, e.Y);
            if (hit.ChartElementType == ChartElementType.DataPoint ||
                hit.ChartElementType == ChartElementType.DataPointLabel)
            {
                pointCurrentlyBeingChanged = (DataPoint)hit.Object;
                chart.Cursor = System.Windows.Forms.Cursors.SizeNS;
            }
        }

        private void chart_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pointCurrentlyBeingChanged != null)
            {
                Values = chart.Series[SeriesName].Points.Select(dp => dp.YValues[0]).ToArray();
                pointCurrentlyBeingChanged = null;
                chart.Cursor = System.Windows.Forms.Cursors.Default;
                chart.Invalidate();
            }
        }
    }
}
