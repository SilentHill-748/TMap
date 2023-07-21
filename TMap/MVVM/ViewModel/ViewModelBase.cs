using FluentValidation.Results;

namespace TMap.MVVM.ViewModel;

public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
    private bool _isValid = false;
    private string _windowTitle;

    public ViewModelBase()
    {
        _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        _windowTitle = "КАРТА ТЕМПЕРАТУРНЫХ РЕЖИМОВ ГЕОЛОГИЧЕСКОГО СРЕЗА ДОРОЖНОЙ КОНСТРУКЦИИ";
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    public event Action? IsValidChanged;

    public string WindowTitle
    {
        get => _windowTitle;
        set => Set(ref _windowTitle, value.ToUpper(), nameof(WindowTitle));
    }
    public virtual bool IsValid
    {
        get => _isValid;
        set 
        {
            Set(ref _isValid, value, nameof(IsValid));
            IsValidChanged?.Invoke();
        }
    }
    public bool HasErrors => _propertyNameToErrorsDictionary.Any();

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName is not { })
            return new List<string>();

        return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
    }

    protected void Validate<TViewMdoel>(AbstractValidator<TViewMdoel> validator, TViewMdoel instance)
    {
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));
        ArgumentNullException.ThrowIfNull(instance, nameof(instance));

        ValidationResult result = validator.Validate(instance);

        ClearErrors();

        AddErrors(result);

        IsValid = !HasErrors;
    }

    private void AddErrors(ValidationResult validationResult)
    {
        var errors = validationResult.Errors;

        foreach (ValidationFailure failure in errors)
        {
            AddError(failure.PropertyName, failure.ErrorMessage);
        }
    }

    private void AddError(string propertyName, string errorMessage)
    {
        if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            _propertyNameToErrorsDictionary[propertyName] = new List<string>();

        _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

        OnErrorsChanged(propertyName);
    }

    private void ClearErrors()
    {
        _propertyNameToErrorsDictionary.Clear();

        OnErrorsChanged();
    }

    [Obsolete("Use Validate<TViewModel> method.")]
    protected void ValidateProperty(Func<bool> predicate, string propertyName, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
        ArgumentNullException.ThrowIfNull(propertyName, nameof(propertyName));
        ArgumentNullException.ThrowIfNull(errorMessage, nameof(errorMessage));

        _propertyNameToErrorsDictionary.Remove(propertyName);

        if (predicate())
        {
            _propertyNameToErrorsDictionary.Add(propertyName, new List<string>() { errorMessage });
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        IsValid = !HasErrors;
    }

    [Obsolete("Use ClearErrors() method.")]
    protected void ClearErrors(string propertyName)
    {
        _propertyNameToErrorsDictionary.Remove(propertyName);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected virtual void Set<T>(ref T field, T value, string propertyName = "")
    {
        if (field is not null && field.Equals(value))
            return;

        field = value;
        OnPropertyChanged(propertyName);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnErrorsChanged(string? propertyName = null)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
