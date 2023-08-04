namespace TMap.Domain.Exceptions;

public class MaterialNotFoundByNameException : Exception
{
    private readonly string _message;

    public MaterialNotFoundByNameException(string materialName)
    {
        ArgumentException.ThrowIfNullOrEmpty(materialName, nameof(materialName));

        _message = $"Материал не найден по названию \'{materialName}\'!";
    }

    public override string Message => _message;
}
