using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.LibraryEditor.ViewModels
{
    internal class MetadataEditorViewModel : INotifyPropertyChanged
    {
        private string name;
        private string category;
        private RelayCommand confirmCommand;
        private bool? dialogResult;

        public MetadataEditorViewModel()
        {
            confirmCommand = new RelayCommand(
                () => DialogResult = true,
                () =>
                    !String.IsNullOrWhiteSpace(Name) &&
                    !String.IsNullOrWhiteSpace(Category) &&
                    ValidateName(Name));
        }

        public MetadataEditorViewModel(LibraryComponent component)
        {
            confirmCommand = new RelayCommand(
                () => DialogResult = true,
                () =>
                    !String.IsNullOrWhiteSpace(Name) &&
                    !String.IsNullOrWhiteSpace(Category) &&
                    ValidateName(Name));
            Category = component.Category;
            Comments = component.Comments;
            DataSource = component.DataSource;
            Name = component.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConfirmCommand => confirmCommand;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                confirmCommand.RaiseCanExecuteChanged();
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
                confirmCommand.RaiseCanExecuteChanged();
            }
        }

        public string Comments { get; set; }
        public string DataSource { get; set; }

        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                dialogResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DialogResult)));
            }
        }

        public bool IsCategoryReadOnly { get; set; }

        public Func<string, bool> ValidateName { get; set; } = _ => true;
    }
}
