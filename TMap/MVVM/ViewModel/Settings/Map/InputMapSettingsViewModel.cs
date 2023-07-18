namespace TMap.MVVM.ViewModel.Settings.Map;

public class InputMapSettingsViewModel : ViewModelBase
{
    private readonly MapSettingsModel _mapSettings;

    private bool _isFrontView;
    private double _envTemerature;

    public InputMapSettingsViewModel(MapSettingsModel mapSettings)
    {
        ArgumentNullException.ThrowIfNull(mapSettings, nameof(mapSettings));

        _mapSettings = mapSettings;

        InitialValidation();

        IsValidChanged += InputMapSettingsViewModel_IsValidChanged;
    }

    private void InputMapSettingsViewModel_IsValidChanged()
    {
        if (IsValid)
        {
            _mapSettings.EnvironmentTemperature = EnvTemperature;
            _mapSettings.IsFrontView = IsFrontView;
        }
    }

    public bool IsFrontView
    {
        get => _isFrontView;
        set
        {
            _mapSettings.IsFrontView = value;
            Set(ref _isFrontView, value, nameof(IsFrontView));
        }
    }
    public double EnvTemperature
    {
        get => _envTemerature;
        set
        {
            _mapSettings.EnvironmentTemperature = value;
            Set(ref _envTemerature, value, nameof(EnvTemperature));
            ValidateProperty(CheckEnvTemp, nameof(EnvTemperature), ValidationErrors.MapSettingsErrors.EnvironmentTemperatureError);
        }
    }

    private void InitialValidation()
    {
        ValidateProperty(CheckEnvTemp, nameof(EnvTemperature), ValidationErrors.MapSettingsErrors.EnvironmentTemperatureError);
    }

    private bool CheckEnvTemp()
        => EnvTemperature <= -70 || EnvTemperature >= 170;
}
