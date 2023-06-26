using System.Collections.ObjectModel;

namespace TMap.MVVM.ViewModel.Settings;

public class InputPipeDataViewModel : ViewModelBase
{
    private const string RadiusError = ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.RadiusError;
    private const string ThicknessError = ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.ThicknessError;
    private const string MaterialTemperatureError = ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.PipeMaterialTemperatureError;
    private const string CoolantTemperatureError = ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.TemperatureError;
    private const string PipeMaterialError = ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.PipeMaterialError;

    private int _radius;
    private int _thickness;
    private double _materialTemperature;
    private double _coolantTemperature;
    private Material? _pipeType;

    public InputPipeDataViewModel(ObservableCollection<Material> pipeMaterials)
    {
        PipeMaterials = pipeMaterials;

        ValidateProperty(() => _radius < 6 || _radius > 27, nameof(Radius), RadiusError);
        ValidateProperty(() => _thickness < 1 || _thickness > 3, nameof(Thickness), ThicknessError);
        ValidateProperty(() => _materialTemperature < -70 || _materialTemperature > 170, nameof(MaterialTemperature), MaterialTemperatureError);
        ValidateProperty(() => _coolantTemperature < 1 || _coolantTemperature > 400, nameof(CoolantTemperature), CoolantTemperatureError);
        ValidateProperty(() => _pipeType is not { }, nameof(PipeType), PipeMaterialError);
    }

    public ObservableCollection<Material> PipeMaterials { get; }

    #region Notify properties
    public int Radius
    {
        get => _radius;
        set
        {
            Set(ref _radius, value, nameof(Radius));
            ValidateProperty(() => _radius < 6 || _radius > 27, nameof(Radius), RadiusError);
        }
    }
    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            ValidateProperty(() => _thickness < 1 || _thickness > 3, nameof(Thickness), ThicknessError);
        }
    }
    public double MaterialTemperature
    {
        get => _materialTemperature;
        set
        {
            Set(ref _materialTemperature, value, nameof(MaterialTemperature));
            ValidateProperty(() => _materialTemperature < -10 || _materialTemperature > 170, nameof(MaterialTemperature), MaterialTemperatureError);
        }
    }
    public double CoolantTemperature
    {
        get => _coolantTemperature;
        set
        {
            Set(ref _coolantTemperature, value, nameof(CoolantTemperature));
            ValidateProperty(() => _coolantTemperature < 1 || _coolantTemperature > 400, nameof(CoolantTemperature), CoolantTemperatureError);
        }
    }
    public Material? PipeType
    {
        get => _pipeType;
        set
        {
            Set(ref _pipeType, value, nameof(PipeType));
            ValidateProperty(() => _pipeType is not { }, nameof(PipeType), PipeMaterialError);
        }
    }
    #endregion
}
