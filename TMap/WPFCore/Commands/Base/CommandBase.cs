namespace TMap.WPFCore.Commands.Base
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object? parameter)
        {
            Execute();
        }

        /// <summary>
        ///     <inheritdoc cref="ICommand.CanExecute(object?)"/>
        /// </summary>
        /// <returns><inheritdoc cref="ICommand.CanExecute(object?)"/></returns>
        public virtual bool CanExecute()
        {
            return true;
        }

        /// <summary>
        ///     <inheritdoc cref="ICommand.Execute(object?)"/>
        /// </summary>
        protected abstract void Execute();
    }
}
