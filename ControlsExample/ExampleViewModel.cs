using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls;

namespace Basilisk.ControlsExample
{
    public class ExampleViewModel : INotifyPropertyChanged
    {
        private double real1;
        private double real2;

        public ExampleViewModel()
        {
            PropertyChanged += (s, e) => { };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [SimulationSetting(DisplayName = "Real Property 1")]
        public double RealProperty1
        {
            get { return real1; }
            set
            {
                real1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RealProperty1"));
            }
        }

        [SimulationSetting(DisplayName = "Real Property 2")]
        public double RealProperty2
        {
            get { return real2; }
            set
            {
                real2 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RealProperty2"));
            }
        }
    }
}
