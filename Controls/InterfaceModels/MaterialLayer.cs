using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    public class MaterialLayer : INotifyPropertyChanged
    {
        private LibraryComponent material;
        private double thickness;

        public LibraryComponent Material
        {
            get { return material; }
            set
            {
                material = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Material)));
            }
        }

        public double Thickness
        {
            get { return thickness; }
            set
            {
                thickness = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Thickness)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
