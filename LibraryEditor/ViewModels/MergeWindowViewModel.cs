using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Basilisk.LibraryEditor.ViewModels
{
    public class MergeWindowViewModel : INotifyPropertyChanged
    {
        private bool? performMerge;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<MergeCollisionViewModel> Collisions { get; set; }

        public bool? PerformMerge
        {
            get { return performMerge; }
            set
            {
                performMerge = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(PerformMerge)));
            }
        }

        public ICommand OkCommand => new RelayCommand(() => PerformMerge = true);
        public ICommand CancelCommand => new RelayCommand(() => PerformMerge = false);
    }
}
