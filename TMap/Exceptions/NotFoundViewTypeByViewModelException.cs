namespace TMap.Exceptions;

public class NotFoundViewTypeByViewModelException : Exception
{
    private readonly string _message;

    public NotFoundViewTypeByViewModelException(Type viewModelType)
    {
        ArgumentNullException.ThrowIfNull(viewModelType, nameof(viewModelType));

        _message = $"Not found type of view by view model \'{viewModelType.FullName}\'!";
    }

    public override string Message => _message;
}
