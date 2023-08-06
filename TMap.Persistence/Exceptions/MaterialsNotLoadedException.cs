namespace TMap.Persistence.Exceptions;

internal class MaterialsNotLoadedException : Exception
{
    private readonly string _message;

    public MaterialsNotLoadedException(string filename, bool canAddFileContent = false)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException(filename);
       
        _message = $"Material wasn't loaded from file \'{filename}\'!";

        if (canAddFileContent)
        {
            string fileContent = File.ReadAllText(filename);

            _message += $"\nFile content:\n{fileContent}";
        }
    }

    public override string Message => _message;
}
