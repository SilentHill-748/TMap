using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TMap.MVVM.ViewModel.Settings;

public class CreatePipeInsulationVewModel : ViewModelBase
{
    private const string ThicknessError = ValidationErrors.PipelineSettingsErrors.InsulationErrors.ThicknessError;
    private const string InitialTemperatureError = ValidationErrors.MaterialErrors.InitTemperatureError;
    private const string InsulationMaterialError = ValidationErrors.MaterialErrors.MaterialError;

    private int _thickness;
    private Material? _insulationMaterial;
    private double _initTemperature;

    public CreatePipeInsulationVewModel(
        ObservableCollection<Material> pipeInsulationMaterials, 
        ObservableCollection<RadialInsulation> insulationCollection)
    {
        ArgumentNullException.ThrowIfNull(insulationCollection, nameof(insulationCollection));
        ArgumentNullException.ThrowIfNull(pipeInsulationMaterials, nameof(pipeInsulationMaterials));

        PipeInsulationMaterials = pipeInsulationMaterials;
        PipeInsulationLayers = insulationCollection;

        CreateInsulationCommand = new CreatePipeInsulationCommand(this);

        InitialValidation();
    }

    public ObservableCollection<Material> PipeInsulationMaterials { get; }
    public ObservableCollection<RadialInsulation> PipeInsulationLayers { get; }

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
            Set(ref _initTemperature, value, nameof(InitialTemperature));
            ValidateProperty(() => _initTemperature < -70 || _initTemperature > 170, nameof(InitialTemperature), InitialTemperatureError);
        }
    }
    public Material? InsulationMaterial
    {
        get => _insulationMaterial;
        set
        {
            Set(ref _insulationMaterial, value, nameof(InsulationMaterial));
            ValidateProperty(() => _insulationMaterial is not { }, nameof(InsulationMaterial), InsulationMaterialError);
        }
    }

    public ICommand CreateInsulationCommand { get; }

    private void InitialValidation()
    {
        ValidateProperty(() => _thickness < 1 || _thickness > 50, nameof(Thickness), ThicknessError);
        ValidateProperty(() => _initTemperature <= -70 || _initTemperature >= 170, nameof(InitialTemperature), InitialTemperatureError);
        ValidateProperty(() => _insulationMaterial is not { }, nameof(InsulationMaterial), InsulationMaterialError);
    }
}
