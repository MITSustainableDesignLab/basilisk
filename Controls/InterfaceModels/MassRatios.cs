using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    public class MassRatios : INotifyPropertyChanged, IMaterialSettable
    {
        private double highLoadRatio;
        private LibraryComponent material;
        private double normalRatio;

        public double HighLoadRatio
        {
            get { return highLoadRatio; }
            set
            {
                highLoadRatio = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighLoadRatio)));
            }
        }

        public LibraryComponent Material
        {
            get { return material; }
            set
            {
                material = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Material)));
            }
        }

        public double NormalRatio
        {
            get { return normalRatio; }
            set
            {
                normalRatio = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NormalRatio)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MassRatios Duplicate() =>
            new MassRatios()
            {
                HighLoadRatio = HighLoadRatio,
                Material = Material,
                NormalRatio = NormalRatio
            };

        public bool TrySetMaterial(LibraryComponent material)
        {
            Material = material;

            return true;
        }
    }
}
