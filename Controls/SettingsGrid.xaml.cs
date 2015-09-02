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
            "Settings",
            typeof(IEnumerable<SimulationSetting>),
            typeof(SettingsGrid));
    }
}
