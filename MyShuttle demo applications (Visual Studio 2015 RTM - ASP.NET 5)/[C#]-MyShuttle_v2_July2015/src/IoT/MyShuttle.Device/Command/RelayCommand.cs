using System;
using System.Windows.Input;

namespace MyShuttle.Device.Command
{
    public class RelayCommand : ICommand
    { 
        private readonly Action<object> _action;

        private RelayCommand() { }

        public RelayCommand(Action<object> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action(parameter);
        }


        public event EventHandler CanExecuteChanged;
    }
    
}
