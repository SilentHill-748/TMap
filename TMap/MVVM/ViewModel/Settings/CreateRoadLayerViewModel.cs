using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TMap.MVVM.ViewModel.Settings;

public class CreateRoadLayerViewModel : ViewModelBase
{
    private const string WidthError = ValidationErrors.RoadSettingsErrors.CreateRoadLayerErrors.WidthError;
    private const string ThicknessError = ValidationErrors.MaterialErrors.ThicknessError;
    private const string MaterialError = ValidationErrors.MaterialErrors.MaterialError;
    private const string InitTemperatureError = ValidationErrors.MaterialErrors.InitTemperatureError;
    private const string HumidityError = ValidationErrors.MaterialErrors.HumidityError;

    private int _width;
    private int _thickness;
    private Material? _material;
    private double _initTemp;
    private double _humidity;

    public CreateRoadLayerViewModel(ObservableCollection<Material> materials)
    {
        ArgumentNullException.ThrowIfNull(materials, nameof(materials));

        Materials = materials;

        AddRoadLayerCommand = new AddRoadLayerCommand(this);

        InitialValidation();
    }

    #region Notify properties
    public int Width
    {
        get => _width;
        set
        {
            Set(ref _width, value, nameof(Width));
            ValidateWidth();
        }
    }
    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            ValidateThickness();
        }
    }
    public Material? Material
    {
        get => _material;
        set
        {
            Set(ref _material, value, nameof(Material));
            ValidateMaterial();
        }
    }
    public double InitialTemperature
    {
        get => _initTemp;
        set
        {
            Set(ref _initTemp, value, nameof(InitialTemperature));
            ValidateTemperature(value);
        }
    }
    public double Humidity
    {
        get => _humidity;
        set
        {
            Set(ref _humidity, value, nameof(Humidity));
            ValidateHumidity();
        }
    }
    #endregion

    public ObservableCollection<Material> Materials { get; }

    public ICommand AddRoadLayerCommand { get; }

    private void InitialValidation()
    {
        // TODO: Рефакторинг валидации.
        ValidateWidth();
        ValidateThickness();
        ValidateMaterial();
        ValidateTemperature(InitialTemperature);
        ValidateHumidity();
    }

    private void ValidateWidth()
        => ValidateProperty(() => Width < 700, nameof(Width), WidthError);
    private void ValidateThickness()
        => ValidateProperty(() => Thickness < 1, nameof(Thickness), ThicknessError);
    private void ValidateHumidity()
        => ValidateProperty(() => Humidity < 0.01, nameof(Humidity), HumidityError);
    private void ValidateMaterial()
        => ValidateProperty(() => Material is not { }, nameof(Material), MaterialError);
    private void ValidateTemperature(double val)
        => ValidateProperty(() => val <= -70 || val >= 170, nameof(InitialTemperature), InitTemperatureError);
}
