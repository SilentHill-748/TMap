namespace TMap.MVVM.ViewModel.Settings.Map;

public class CreateMapLayerViewModel : ViewModelBase
{
    #region Dependencies
    private readonly MaterialStore _materialStore;
    private readonly CreateMapLayerValidator _validator;
    #endregion

    #region Private fields
    private int _thickness;
    private MaterialModel? _material;
    private double _humidity;
    private double _initTemperature;
    #endregion

    public CreateMapLayerViewModel(MaterialStore materialStore, CreateMapLayerValidator validator)
    {
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        _materialStore = materialStore;
        _validator = validator;

        Materials = new ObservableCollection<MaterialModel>(materialStore.GetMapMaterials());

        AddLayerCommand = new AddMapLayerCommand(this);

        _materialStore.StoreChanged += MaterialStore_StoreChanged;
        PropertyChanged += CreateMapLayerViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public ObservableCollection<MaterialModel> Materials { get; }
    #endregion

    #region Notify properties
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
    public double Humidity
    {
        get => _humidity;
        set => Set(ref _humidity, value, nameof(Humidity));
    }
    public double InitTemperature
    {
        get => _initTemperature;
        set => Set(ref _initTemperature, value, nameof(InitTemperature));
    }
    #endregion

    #region Commands
    public ICommand AddLayerCommand { get; }
    #endregion

    #region Event handlers
    private void MaterialStore_StoreChanged()
    {
        Materials.UpdateCollection(_materialStore.GetMapMaterials());
    }

    private void CreateMapLayerViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}
