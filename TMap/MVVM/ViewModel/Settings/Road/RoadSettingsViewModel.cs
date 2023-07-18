namespace TMap.MVVM.ViewModel.Settings.Road;

public class RoadSettingsViewModel : ViewModelBase
{
    private readonly SettingsModel _settings;
    private readonly CreateRoadLayerViewModel _createRoadLayerViewModel;
    private readonly InputRoadSettingsViewModel _inputRoadSettingsViewModel;
    private readonly MaterialStore _materialStore;

    private int _viewTitleFontSize;

    public RoadSettingsViewModel(
        SettingsModel settings,
        MaterialStore materialStore,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _settings = settings;
        _materialStore = materialStore;
        Materials = new ObservableCollection<MaterialModel>(materialStore.GetRoadMaterials());
        _createRoadLayerViewModel = new CreateRoadLayerViewModel(Materials);
        _inputRoadSettingsViewModel = new InputRoadSettingsViewModel(settings);

        WindowTitle = "Настройка гелогоческого среза дорожной конструкции";
        ViewTitleFontSize = 22;

        NavigateNextCommand = new NavigateCommand<PipeSettingsViewModel>(navigationService, () => HasNext);
        NavigateBackCommand = new NavigateCommand<MapSettingsViewModel>(navigationService);
        RemoveRoadLayerCommand = new RemoveRoadLayerCommand(this);

        _inputRoadSettingsViewModel.IsValidChanged += InputRoadSettingsViewModel_IsValidChanged;
        _materialStore.StoreChanged += MaterialStore_StoreChanged;
        Settings.Layers.CollectionChanged += Layers_CollectionChanged;

        WeakReferenceMessenger.Default.Register<CreateRoadLayerMessage>(this, LayerCreated);
    }

    #region Public properties
    public ObservableCollection<MaterialModel> Materials { get; }
    public CreateRoadLayerViewModel CreateRoadLayerView => _createRoadLayerViewModel;
    public InputRoadSettingsViewModel InputRoadSettingsView => _inputRoadSettingsViewModel;
    public RoadSettingsModel Settings => _settings.RoadSettings;
    #endregion

    #region Notify properties
    public string ViewTitle => WindowTitle;
    public int ViewTitleFontSize
    {
        get => _viewTitleFontSize;
        set => Set(ref _viewTitleFontSize, value, nameof(ViewTitleFontSize));
    }
    public bool IsInvalidLayerCount => Settings.Layers.Count < 1;
    public bool HasNext => !IsInvalidLayerCount && InputRoadSettingsView.IsValid;
    #endregion

    public ICommand NavigateNextCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public ICommand RemoveRoadLayerCommand { get; }

    private void LayerCreated(object recipient, CreateRoadLayerMessage message)
    {
        if (message is not { })
            throw new Exception("Ошибка сохранения созданного слоя дорожной конструкции!");

        Settings.Layers.Add(message.Value);
        OnPropertyChanged(nameof(HasNext));
        OnPropertyChanged(nameof(IsInvalidLayerCount));
    }

    private void MaterialStore_StoreChanged()
    {
        Materials.UpdateCollection(_materialStore.GetRoadMaterials());
    }

    private void InputRoadSettingsViewModel_IsValidChanged()
    {
        OnPropertyChanged(nameof(IsInvalidLayerCount));
        OnPropertyChanged(nameof(HasNext));
    }

    private void Layers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsInvalidLayerCount));
        OnPropertyChanged(nameof(HasNext));
    }
}