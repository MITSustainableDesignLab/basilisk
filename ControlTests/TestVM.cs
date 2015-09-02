using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls;

namespace Basilisk.ControlTests
{
    internal class TestVM : INotifyPropertyChanged
    {
        private TestEnum enumSetting;
        private int integerSetting;
        private double realSetting;
        private string stringSetting;

        private double notASetting;

        public TestVM()
        {
            PropertyChanged += (s, e) => { };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [SimulationSetting]
        public TestEnum EnumSetting
        {
            get { return enumSetting; }
            set
            {
                enumSetting = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EnumProperty"));
            }
        }

        [SimulationSetting]
        public int IntegerSetting
        {
            get { return integerSetting; }
            set
            {
                integerSetting = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IntegerSetting"));
            }
        }

        public double NotASetting
        {
            get { return notASetting; }
            set
            {
                notASetting = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NotASetting"));
            }
        }

        [SimulationSetting(DisplayName = "Real Setting")]
        public double RealSetting
        {
            get { return realSetting; }
            set
            {
                realSetting = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RealSetting"));
            }
        }

        [SimulationSetting]
        public string StringSetting
        {
            get { return stringSetting; }
            set
            {
                stringSetting = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StringSetting"));
            }
        }

        public enum TestEnum
        {
            FirstChoice,
            SecondChoice
        }
    }
}
