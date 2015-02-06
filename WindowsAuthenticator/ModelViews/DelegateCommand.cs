using System;
using System.Windows.Input;

namespace WindowsAuthenticator.ModelViews
{
    internal class DelegateCommand : ICommand
    {
        private static readonly Predicate<object> DefaultCanExecute = ((arg) => true);

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public DelegateCommand(Action<object> onExecuteMethod, Predicate<object> onCanExecuteMethod = null)
        {
            if (onExecuteMethod == null) throw new ArgumentNullException("onExecuteMethod");

            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod ?? DefaultCanExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
