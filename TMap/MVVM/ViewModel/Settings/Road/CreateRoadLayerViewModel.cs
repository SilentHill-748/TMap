namespace TMap.MVVM.ViewModel.Settings.Road;

public class CreateRoadLayerViewModel : ViewModelBase
{
    #region Dependencies
    private readonly CreateRoadLayerValidator _validator;
    #endregion

    #region private fields
    private int _width;
    private int _thickness;
    private MaterialModel? _material;
    private double _initTemp;
    private double _humidity;
    #endregion

    public CreateRoadLayerViewModel(ObservableCollection<MaterialModel> roadMaterials, CreateRoadLayerValidator validator)
    {
        ArgumentNullException.ThrowIfNull(roadMaterials, nameof(roadMaterials));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        Materials = roadMaterials;
        _validator = validator;

        AddRoadLayerCommand = new AddRoadLayerCommand(this);

        PropertyChanged += CreateRoadLayerViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public ObservableCollection<MaterialModel> Materials { get; }
    #endregion

    #region Notify properties
    public int Width
    {
        get => _width;
        set => Set(ref _width, value, nameof(Width));
    }
    public int Thickness
    {
        get => _thickness;
        set => Set(ref _thickness, value, nameof(Thickness));
    }
    public MaterialModel? Material
    {
        get => _material;
        set => Set(ref _material, value, nameof(Material));
    }
    public double InitialTemperature
    {
        get => _initTemp;
        set => Set(ref _initTemp, value, nameof(InitialTemperature));
    }
    public double Humidity
    {
        get => _humidity;
        set => Set(ref _humidity, value, nameof(Humidity));
    }
    #endregion

    #region Commands
    public ICommand AddRoadLayerCommand { get; }
    #endregion

    #region Event handlers
    private void CreateRoadLayerViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}