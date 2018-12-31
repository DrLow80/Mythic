using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Mythic.Project.Roller
{
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }
    }

    public class RelayCommand<T> : ICommand
    {
        protected Predicate<T> canExecute;
        protected Action<T> execute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        protected RelayCommand()
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute((T) parameter);
        }

        public void Execute(object parameter)
        {
            execute((T) parameter);
        }
    }
}