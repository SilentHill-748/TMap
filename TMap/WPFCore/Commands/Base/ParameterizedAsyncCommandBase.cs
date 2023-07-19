namespace TMap.WPFCore.Commands.Base;

public abstract class ParameterizedAsyncCommandBase<TParameter> : ICommand
{
    private readonly Action<Exception> _exceptionHandler;
    private bool _isExecuting;

    public ParameterizedAsyncCommandBase(Action<Exception> exceptionHandler)
    {
        ArgumentNullException.ThrowIfNull(exceptionHandler, nameof(exceptionHandler));

        _exceptionHandler = exceptionHandler;
    }

    public bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting = value;
            OnCanExecuteChanged();
        }
    }

    private void OnCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;

    bool ICommand.CanExecute(object? parameter)
    {
        return CanExecute(CastObjectToT(parameter));
    }

    async void ICommand.Execute(object? parameter)
    {
        try
        {
            IsExecuting = true;
            await ExecuteAsync(CastObjectToT(parameter));
        }
        catch (Exception ex)
        {
            _exceptionHandler?.Invoke(ex);
        }
        finally
        {
            IsExecuting = false;
        }
    }

    /// <summary>
    ///     <inheritdoc cref="ICommand.CanExecute(object?)"/>
    /// </summary>
    /// <param name="parameter"><inheritdoc cref="ICommand.CanExecute(object?)"/></param>
    /// <returns><inheritdoc cref="ICommand.CanExecute(object?)"/></returns>
    public virtual bool CanExecute(TParameter parameter)
    {
        return !IsExecuting;
    }

    /// <summary>
    ///     <inheritdoc cref="ICommand.Execute(object?)"/>
    /// </summary>
    /// <param name="parameter"><inheritdoc cref="ICommand.Execute(object?)"/></param>
    /// <returns><see cref="Task"/></returns>
    protected abstract Task ExecuteAsync(TParameter parameter);

    private static TParameter CastObjectToT(object? parameter)
    {
        //TODO: Напиши свой класс исключения для параметризированных команд, когда object parameter != T.
        if (parameter is TParameter p)
            return p;

        throw new ArgumentException($"Object parameter is null or is not generic type \'{typeof(TParameter).Name}\'!", nameof(parameter));
    }
}
