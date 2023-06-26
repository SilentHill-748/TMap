using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TMap.WPFCore.Commands.Base;

public abstract class AsyncCommandBase : ICommand
{
    private readonly Action<Exception> _exceptionHandler;
    private bool _isExecuting;

    public AsyncCommandBase(Action<Exception> exceptionHandler)
    {
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

    public event EventHandler? CanExecuteChanged;

    bool ICommand.CanExecute(object? parameter)
    {
        return CanExecute();
    }

    async void ICommand.Execute(object? parameter)
    {
        try
        {
            IsExecuting = true;
            await ExecuteAsync();
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
    /// <returns><inheritdoc cref="ICommand.CanExecute(object?)"/></returns>
    public virtual bool CanExecute()
    {
        return !IsExecuting;
    }

    /// <summary>
    ///     <inheritdoc cref="ICommand.Execute(object?)"/>
    /// </summary>
    /// <returns>Task without awaitable value.</returns>
    protected abstract Task ExecuteAsync();

    protected void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
