using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TMap.MVVM.ViewModel.Settings;

public class CreateChannelInsulationViewModel : ViewModelBase
{
    private const string ThicknessError = ValidationErrors.PipelineSettingsErrors.InsulationErrors.ThicknessError;
    private const string MaterialError = ValidationErrors.MaterialErrors.MaterialError;
    private const string InitialTemperatureError = ValidationErrors.MaterialErrors.InitTemperatureError;

    private int _thickness;
    private double _initTemperature;
    private MaterialModel? _material;

    public CreateChannelInsulationViewModel(
        ObservableCollection<MaterialModel> channelInsulationMaterials,
        ObservableCollection<ChannelInsulation> insulationCollection)
    {
        ArgumentNullException.ThrowIfNull(channelInsulationMaterials, nameof(channelInsulationMaterials));
        ArgumentNullException.ThrowIfNull(insulationCollection, nameof(insulationCollection));

        ChannelInsulationMaterials = channelInsulationMaterials;
        ChannelInsulationCollection = insulationCollection;

        CreateChannelInsulationCommand = new CreateChannelInsulationCommand(this);

        InitialValidation();
    }

    public ObservableCollection<MaterialModel> ChannelInsulationMaterials { get; }
    public ObservableCollection<ChannelInsulation> ChannelInsulationCollection { get; }

    #region Notify properties
    public MaterialModel? Material
    {
        get => _material;
        set
        {
            Set(ref _material, value, nameof(Material));
            ValidateProperty(() => _material is not { }, nameof(Material), MaterialError);
        }
    }
    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            ValidateProperty(() => _thickness < 1 || _thickness > 50, nameof(Thickness), ThicknessError);
        }
    }
    public double InitialTemperature
    {
        get => _initTemperature;
        set
        {
            var temp = value;
            Set(ref _initTemperature, value, nameof(InitialTemperature));
            ValidateProperty(() => temp < -70 || temp > 170, nameof(InitialTemperature), InitialTemperatureError);
        }
    }
    #endregion

    public ICommand CreateChannelInsulationCommand { get; }

    private void InitialValidation()
    {
        ValidateProperty(() => _material is not { }, nameof(Material), MaterialError);
        ValidateProperty(() => _thickness < 1 || _thickness > 500, nameof(Thickness), ThicknessError);
        ValidateProperty(() => InitialTemperature < -70 || InitialTemperature > 170, nameof(InitialTemperature), InitialTemperatureError);
    }
}
