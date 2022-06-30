using System;
using System.Diagnostics;
using System.Windows.Input;

namespace DocumentVisor.Infrastructure
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool TryExecute()
        {
            if (!this.CanExecute())
            {
                return false;
            }

            this.Execute();
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        public abstract bool CanExecute();

        public abstract void Execute();
    }

    public abstract class CommandBase<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool TryExecute(T parameter)
        {
            if (!this.CanExecute(parameter))
            {
                return false;
            }

            this.Execute(parameter);
            return true;
        }

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute((T)parameter);
        }

        [DebuggerStepThrough]
        public abstract bool CanExecute(T parameter);

        public abstract void Execute(T parameter);
    }
}
