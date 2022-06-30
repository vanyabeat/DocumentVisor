using System;
using System.Diagnostics;
using System.Windows.Input;

namespace DocumentVisor.Infrastructure
{
    public class RelayCommand : CommandBase
    {
        private readonly Action execute;

        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute()
        {
            return this.canExecute == null || this.canExecute();
        }

        public override void Execute()
        {
            this.execute();
        }
    }

    public class RelayCommand<T> : CommandBase<T>
    {
        private readonly Action<T> execute;

        private readonly Predicate<T> canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }
    
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public override bool CanExecute(T parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public override void Execute(T parameter)
        {
            this.execute(parameter);
        }
    }
}
