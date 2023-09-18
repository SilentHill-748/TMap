namespace TMap.Services;

internal sealed class ViewToViewModelDataTemplateGeneratorService
{
    private readonly Assembly _targetAssembly;
    private readonly IEnumerable<Type> _viewModels;
    private readonly IEnumerable<Type> _views;

    public ViewToViewModelDataTemplateGeneratorService()
    {
        _targetAssembly = typeof(ViewToViewModelDataTemplateGeneratorService).Assembly;
        
        _viewModels = _targetAssembly
            .GetTypes()
            .Where(type => type.Name.EndsWith("ViewModel"));

        _views = _targetAssembly
            .GetTypes()
            .Where(type => type.Name.EndsWith("View"));
    }

    public IEnumerable<DataTemplate> GenerateTemplates()
    {
        foreach (Type viewModelType in _viewModels)
        {
            if (CheckViewType(viewModelType))
                yield return GenerateTemplate(viewModelType);
        }
    }

    private DataTemplate GenerateTemplate(Type viewModelType)
    {
        Type viewType = GetViewTypeByViewModelType(viewModelType)
            ?? throw new NotFoundViewTypeByViewModelException(viewModelType);

        return new DataTemplate(viewModelType)
        {
            VisualTree = new FrameworkElementFactory(viewType)
        };
    }

    private bool CheckViewType(Type viewModelType)
    {
        return GetViewTypeByViewModelType(viewModelType) is not null;
    }

    private Type? GetViewTypeByViewModelType(Type viewModelType)
    {
        string viewTypeName = viewModelType.Name!.Replace("ViewModel", "View");

        return _views.FirstOrDefault(type => type.Name.Equals(viewTypeName));
    }
}
