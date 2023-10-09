namespace TMap.MVVM.ViewModel.Settings.Pipeline;

public class CreatePipeInsulationViewModel : ViewModelBase
{
    #region Dependencies
    private readonly CreatePipeInsulationValidator _validator;
    #endregion

    #region Private fields
    private int _thickness;
    private MaterialModel? _insulationMaterial;
    private double _initTemperature;
    #endregion

    public CreatePipeInsulationViewModel(
        ObservableCollection<MaterialModel> pipeInsulationMaterials, 
        ObservableCollection<RadialInsulation> insulationCollection,
        CreatePipeInsulationValidator validator)
    {
        ArgumentNullException.ThrowIfNull(insulationCollection, nameof(insulationCollection));
        ArgumentNullException.ThrowIfNull(pipeInsulationMaterials, nameof(pipeInsulationMaterials));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        PipeInsulationMaterials = pipeInsulationMaterials;
        PipeInsulationLayers = insulationCollection;
        _validator = validator;

        CreateInsulationCommand = new CreatePipeInsulationCommand(this);

        PropertyChanged += CreatePipeInsulationViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public ObservableCollection<MaterialModel> PipeInsulationMaterials { get; }
    public ObservableCollection<RadialInsulation> PipeInsulationLayers { get; }
    #endregion

    #region Notify properties
    public int Thickness
    {
        get => _thickness;
        set => Set(ref _thickness, value, nameof(Thickness));
    }
    public double InitialTemperature
    {
        get => _initTemperature;
        set => Set(ref _initTemperature, value, nameof(InitialTemperature));
    }
    public MaterialModel? InsulationMaterial
    {
        get => _insulationMaterial;
        set => Set(ref _insulationMaterial, value, nameof(InsulationMaterial));
    }
    #endregion

    #region Commands
    public ICommand CreateInsulationCommand { get; }
    #endregion

    #region Event handlers
    private void CreatePipeInsulationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}
