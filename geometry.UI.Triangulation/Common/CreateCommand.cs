using System;
using System.Windows.Input;

namespace geometry.UI.Triangulation.Common
{
    public class CreateCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;
        public CreateCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => canExecute == null || canExecute((T)parameter);
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public void Execute(object parameter) => execute(parameter == null ? default : (T)Convert.ChangeType(parameter, typeof(T)));
    }

    public class CreateCommand : ICommand
    {
        private readonly Action execute;
        public CreateCommand(Action execute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
        }
        public bool CanExecute(object parameter) => true;
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public void Execute(object parameter) => execute();
    }
}
