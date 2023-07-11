using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TMap.MVVM.ViewModel.Settings;

public class CreateLayerViewModel : ViewModelBase
{
    private const string ThicknessError = ValidationErrors.MaterialErrors.ThicknessError;
    private const string MaterialError = ValidationErrors.MaterialErrors.MaterialError;
    private const string HumidityError = ValidationErrors.MaterialErrors.HumidityError;
    private const string InitTemperatureError = ValidationErrors.MaterialErrors.InitTemperatureError;

    private int _thickness;
    private MaterialModel? _material;
    private double _humidity;
    private double _initTemperature;

    public CreateLayerViewModel(ObservableCollection<MaterialModel> materials)
    {
        Materials = materials;

        AddLayerCommand = new AddMapLayerCommand(this, OnLayerCreated);

        InitialValidation();
    }

    public event Action<Layer>? LayerCreated;

    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            ValidateProperty(() => Thickness < 1, nameof(Thickness), ThicknessError);
        }
    }
    public MaterialModel? Material
    {
        get => _material;
        set
        {
            Set(ref _material, value, nameof(Material));
            ValidateProperty(() => Material is not { }, nameof(Material), MaterialError);
        }
    }
    public double Humidity
    {
        get => _humidity;
        set
        {
            Set(ref _humidity, value, nameof(Humidity));
            ValidateProperty(() => Humidity < 0.01, nameof(Humidity), HumidityError);
        }
    }
    public double InitTemperature
    {
        get => _initTemperature;
        set
        {
            Set(ref _initTemperature, value, nameof(InitTemperature));
            ValidateProperty(CheckInitTemp, nameof(InitTemperature), InitTemperatureError);
        }
    }

    public ObservableCollection<MaterialModel> Materials { get; }

    public ICommand AddLayerCommand { get; }

    private void OnLayerCreated(Layer layer)
    {
        LayerCreated?.Invoke(layer);
        Material = default;
        Thickness = default;
        Humidity = default;
        InitTemperature = default;
    }

    private bool CheckInitTemp()
        => InitTemperature <= -70 || InitTemperature >= 170;
    
    private void InitialValidation()
    {
        ValidateProperty(() => Material is not { }, nameof(Material), MaterialError);
        ValidateProperty(() => Thickness < 1, nameof(Thickness), ThicknessError);
        ValidateProperty(() => Humidity < 0.01, nameof(Humidity), HumidityError);
        ValidateProperty(CheckInitTemp, nameof(InitTemperature), InitTemperatureError);
    }
}
