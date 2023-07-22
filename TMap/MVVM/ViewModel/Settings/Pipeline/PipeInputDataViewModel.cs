namespace TMap.MVVM.ViewModel.Settings.Pipeline;

public class PipeInputDataViewModel : ViewModelBase
{
    #region Dependencies
    private readonly PipeInputDataValidator _validator;
    #endregion

    #region Private fields
    private int _radius;
    private int _thickness;
    private double _materialTemperature;
    private double _coolantTemperature;
    private MaterialModel? _pipeType;
    #endregion

    public PipeInputDataViewModel(ObservableCollection<MaterialModel> pipeMaterials, PipeInputDataValidator validator)
    {
        ArgumentNullException.ThrowIfNull(pipeMaterials, nameof(pipeMaterials));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        PipeMaterials = pipeMaterials;
        _validator = validator;

        PropertyChanged += PipeInputDataViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public ObservableCollection<MaterialModel> PipeMaterials { get; }
    #endregion

    #region Notify properties
    public int Radius
    {
        get => _radius;
        set => Set(ref _radius, value, nameof(Radius));
    }
    public int Thickness
    {
        get => _thickness;
        set => Set(ref _thickness, value, nameof(Thickness));
    }
    public double MaterialTemperature
    {
        get => _materialTemperature;
        set => Set(ref _materialTemperature, value, nameof(MaterialTemperature));
    }
    public double CoolantTemperature
    {
        get => _coolantTemperature;
        set => Set(ref _coolantTemperature, value, nameof(CoolantTemperature));
    }
    public MaterialModel? PipeType
    {
        get => _pipeType;
        set => Set(ref _pipeType, value, nameof(PipeType));
    }
    #endregion

    #region Event handlers
    private void PipeInputDataViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}
