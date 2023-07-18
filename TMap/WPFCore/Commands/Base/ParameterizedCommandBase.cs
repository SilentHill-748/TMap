namespace TMap.WPFCore.Commands.Base
{
    public abstract class ParameterizedCommandBase<TParameter> : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute(CastObjectToT(parameter));
        }

        void ICommand.Execute(object? parameter)
        {
            Execute(CastObjectToT(parameter));
        }

        /// <summary>
        ///     <inheritdoc cref="ICommand.CanExecute(object?)"/>
        /// </summary>
        /// <param name="parameter"><inheritdoc cref="ICommand.CanExecute(object?)"/></param>
        /// <returns><inheritdoc cref="ICommand.CanExecute(object?)"/></returns>
        public virtual bool CanExecute(TParameter parameter)
        {
            return true;
        }

        /// <summary>
        ///     <inheritdoc cref="ICommand.Execute(object?)"/>
        /// </summary>
        /// <param name="parameter"><inheritdoc cref="ICommand.Execute(object?)"/></param>
        protected abstract void Execute(TParameter parameter);

        private static TParameter CastObjectToT(object? parameter)
        {
            if (parameter is TParameter p)
                return p;

            throw new ArgumentException($"Object parameter is null or is not generic type \'{typeof(TParameter).Name}\'!", nameof(parameter));
        }
    }
}
