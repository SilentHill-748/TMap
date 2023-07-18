namespace TMap.MVVM.ViewModel.Settings.Map;

public class MapSettingsViewModel : ViewModelBase
{
    #region Private fields
    private readonly NavigationService _navigationService;
    private readonly ObservableCollection<MaterialModel> _materials;
    private readonly MapSettingsModel _settings;
    private readonly MaterialStore _materialStore;

    private readonly CreateLayerViewModel _createLayerViewModel;
    private readonly InputMapSettingsViewModel _inputMapSettingsViewModel;

    private int _titleFontSize;
    #endregion

    public MapSettingsViewModel(
        SettingsModel settings,
        MaterialStore materialStore,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _materialStore = materialStore;
        _materials = new ObservableCollection<MaterialModel>(materialStore.GetMapMaterials());
        _settings = settings.MapSettings;
        _navigationService = navigationService;
        _createLayerViewModel = new CreateLayerViewModel(materialStore);
        _inputMapSettingsViewModel = new InputMapSettingsViewModel(settings.MapSettings);

        WindowTitle = "Настройка карты геологического среза";
        TitleFontSize = 22;

        _settings.MapSoilLayers.CollectionChanged += MapSoilLayers_CollectionChanged;
        _createLayerViewModel.LayerCreated += CreateLayerViewModel_LayerCreated;
        _inputMapSettingsViewModel.IsValidChanged += InputMapSettingsViewModel_IsValidChanged;
        _materialStore.StoreChanged += MaterialStore_StoreChanged;

        InitCommands();
    }

    #region Public properties
    public ObservableCollection<MaterialModel> Materials => _materials;
    public MapSettingsModel Settings => _settings;
    public CreateLayerViewModel CreateLayerView => _createLayerViewModel;
    public InputMapSettingsViewModel InputMapSettingsView => _inputMapSettingsViewModel;
    #endregion

    #region Notify properties
    public bool IsInvalidMapHeight => _settings.MapHeight < 100;
    public string ViewTitle => WindowTitle;
    public int TitleFontSize
    {
        get => _titleFontSize;
        set => Set(ref _titleFontSize, value, nameof(_titleFontSize));
    }
    public bool HasNext => ValidateKeyProperties();
    #endregion

    #region Commands
    public ICommand? RemoveLayerCommand { get; private set; }
    public ICommand? NavigateNextCommand { get; private set; }
    #endregion

    #region Private methods
    private void CreateLayerViewModel_LayerCreated(Layer layer)
    {
        Settings.MapSoilLayers.Add(layer);
    }

    private void MaterialStore_StoreChanged()
    {
        Materials.UpdateCollection(_materialStore.GetMapMaterials());
    }

    private void MapSoilLayers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsInvalidMapHeight));
        OnPropertyChanged(nameof(HasNext));
    }

    private void InputMapSettingsViewModel_IsValidChanged()
    {
        OnPropertyChanged(nameof(IsInvalidMapHeight));
        OnPropertyChanged(nameof(HasNext));
    }

    private void InitCommands()
    {
        NavigateNextCommand = new NavigateCommand<RoadSettingsViewModel>(_navigationService, () => HasNext);
        RemoveLayerCommand = new RemoveMapLayerCommand(this);
    }

    private bool ValidateKeyProperties()
    {
        var isValidInput = InputMapSettingsView.IsValid;

        return !IsInvalidMapHeight && isValidInput;
    }
    #endregion
}