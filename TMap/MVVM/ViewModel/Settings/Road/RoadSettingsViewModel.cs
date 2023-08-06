using TMap.Exceptions;

namespace TMap.MVVM.ViewModel.Settings.Road;

public class RoadSettingsViewModel : ViewModelBase
{
    #region Dependencies
    private readonly SettingsModel _settings;
    private readonly CreateRoadLayerViewModel _createRoadLayerViewModel;
    private readonly RoadInputDataViewModel _inputRoadSettingsViewModel;
    private readonly MaterialStore _materialStore;
    #endregion

    #region Private fields
    private int _viewTitleFontSize;
    #endregion

    public RoadSettingsViewModel(
        SettingsModel settings,
        MaterialStore materialStore,
        NavigationService navigationService,
        CreateRoadLayerValidator createRoadLayerValidator,
        RoadInputDataValidator roadInputDataValidator)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));
        ArgumentNullException.ThrowIfNull(createRoadLayerValidator, nameof(createRoadLayerValidator));
        ArgumentNullException.ThrowIfNull(roadInputDataValidator, nameof(roadInputDataValidator));

        _settings = settings;
        _materialStore = materialStore;
        Materials = new ObservableCollection<MaterialModel>(materialStore.GetRoadMaterials());
        _createRoadLayerViewModel = new CreateRoadLayerViewModel(Materials, createRoadLayerValidator);
        _inputRoadSettingsViewModel = new RoadInputDataViewModel(settings, roadInputDataValidator);

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
    public RoadInputDataViewModel InputRoadSettingsView => _inputRoadSettingsViewModel;
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

    #region Commands
    public ICommand NavigateNextCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public ICommand RemoveRoadLayerCommand { get; }
    #endregion

    #region Event handlers
    private void LayerCreated(object recipient, CreateRoadLayerMessage message)
    {
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

    private void Layers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsInvalidLayerCount));
        OnPropertyChanged(nameof(HasNext));
    }
    #endregion
}