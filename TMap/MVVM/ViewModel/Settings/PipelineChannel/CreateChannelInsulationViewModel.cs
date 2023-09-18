namespace TMap.MVVM.ViewModel.Settings.PipelineChannel;

public class CreateChannelInsulationViewModel : ViewModelBase
{
    #region Dependencies
    private readonly CreateChannelInsulationValidator _validator;
    #endregion

    #region Private fields
    private int _thickness;
    private double _initTemperature;
    private MaterialModel _material;
    #endregion

    public CreateChannelInsulationViewModel(
        ObservableCollection<MaterialModel> channelInsulationMaterials,
        ObservableCollection<ChannelInsulation> insulationCollection,
        CreateChannelInsulationValidator validator)
    {
        ArgumentNullException.ThrowIfNull(channelInsulationMaterials, nameof(channelInsulationMaterials));
        ArgumentNullException.ThrowIfNull(insulationCollection, nameof(insulationCollection));

        ChannelInsulationMaterials = channelInsulationMaterials;
        ChannelInsulationCollection = insulationCollection;
        _validator = validator;
        _material = new MaterialModel();

        CreateChannelInsulationCommand = new CreateChannelInsulationCommand(this);

        PropertyChanged += CreateChannelInsulationViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public ObservableCollection<MaterialModel> ChannelInsulationMaterials { get; }
    public ObservableCollection<ChannelInsulation> ChannelInsulationCollection { get; }
    #endregion

    #region Notify properties
    public MaterialModel Material
    {
        get => _material;
        set => Set(ref _material, value, nameof(Material));
    }
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
    #endregion

    #region Commands
    public ICommand CreateChannelInsulationCommand { get; }
    #endregion

    #region Event handlers
    private void CreateChannelInsulationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}