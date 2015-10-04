using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Basilisk.Controls;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.LibraryEditor.ViewModels
{
    internal class ComponentPickerViewModel : INotifyPropertyChanged
    {
        private ComponentCategoryCollection components;
        private bool? dialogResult;
        private LibraryComponent selectedComponent;

        public event PropertyChangedEventHandler PropertyChanged;

        public ComponentCategoryCollection Components
        {
            get { return components; }
            set
            {
                components = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Components)));
            }
        }

        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                dialogResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DialogResult)));
            }
        }

        public LibraryComponent SelectedComponent
        {
            get { return selectedComponent; }
            set
            {
                if (value != null)
                {
                    selectedComponent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedComponent)));
                    DialogResult = true;
                }
            }
        }
    }
}
