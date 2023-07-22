namespace TMap.MVVM.ViewModel.Settings.Map;

public class MapInputDataViewModel : ViewModelBase
{
    #region Dependencies
    private readonly MapSettingsModel _mapSettings;
    private readonly MapInputDataValidator _validator;
    #endregion

    #region Private fields
    private bool _isFrontView;
    private double _envTemerature;
    #endregion

    public MapInputDataViewModel(MapSettingsModel mapSettings, MapInputDataValidator validator)
    {
        ArgumentNullException.ThrowIfNull(mapSettings, nameof(mapSettings));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        _mapSettings = mapSettings;
        _validator = validator;

        IsValidChanged += InputMapSettingsViewModel_IsValidChanged;
        PropertyChanged += MapInputDataViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Notify properties
    public bool IsFrontView
    {
        get => _isFrontView;
        set => Set(ref _isFrontView, value, nameof(IsFrontView));
    }
    public double EnvTemperature
    {
        get => _envTemerature;
        set => Set(ref _envTemerature, value, nameof(EnvTemperature));
    }
    #endregion

    #region Event handlers
    private void InputMapSettingsViewModel_IsValidChanged()
    {
        if (IsValid)
        {
            _mapSettings.EnvironmentTemperature = EnvTemperature;
            _mapSettings.IsFrontView = IsFrontView;
        }
    }

    private void MapInputDataViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}
