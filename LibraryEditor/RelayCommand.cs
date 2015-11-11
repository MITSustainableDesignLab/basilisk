using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Basilisk.LibraryEditor
{
    public class RelayCommand : ICommand
    {
        private Func<object, bool> canExecute = _ => true;
        private Action<object> execute;

        public RelayCommand(Action a)
        {
            execute = _ => a();
        }

        public RelayCommand(Action<object> a)
        {
            this.execute = a;
        }

        public RelayCommand(Action<object> a, Func<bool> canExecute)
        {
            this.execute = a;
            this.canExecute = _ => canExecute();
        }

        public RelayCommand(Action a, Func<bool> canExecute)
        {
            this.execute = _ => a();
            this.canExecute = _ => canExecute();
        }

        public RelayCommand(Action<object> a, Func<object, bool> canExecute)
        {
            this.execute = a;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
