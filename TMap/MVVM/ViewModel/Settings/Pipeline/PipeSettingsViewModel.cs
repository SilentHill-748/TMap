namespace TMap.MVVM.ViewModel.Settings.Pipeline;

public class PipeSettingsViewModel : ViewModelBase
{
    private readonly MaterialStore _materialStore;

    private int _viewTitleFontSize;

    public PipeSettingsViewModel(SettingsModel settings, MaterialStore materialStore, NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _materialStore = materialStore;

        Settings = settings.PipelineSettings;
        WindowTitle = "Настройка труб для коллектора трубопровода";
        ViewTitleFontSize = 22;
        PipeInsulationCollection = new ObservableCollection<RadialInsulation>();
        PipeInsulationMaterials = new ObservableCollection<MaterialModel>(materialStore.GetInsulationMaterials());
        PipeMaterials = new ObservableCollection<MaterialModel>(materialStore.GetPipeMaterials());

        InputPipeDataView = new PipeInputDataViewModel(PipeMaterials);
        CreatePipeInsulationView = new CreatePipeInsulationVewModel(PipeInsulationMaterials, PipeInsulationCollection);

        Settings.Channel.Pipes.CollectionChanged += Pipes_CollectionChanged;
        InputPipeDataView.IsValidChanged += PipeSettingsViewModel_IsValidChanged;

        CreatePipeCommand = new CreatePipeCommand(this);
        RemovePipeCommand = new RemovePipeCommand(this);
        RemovePipeInsulationLayerCommand = new RemovePipeInsulationLayerCommand(this);
        NavigateBackCommand = new NavigateCommand<RoadSettingsViewModel>(navigationService);
        NavigateNextCommand = new NavigateCommand<PipelineChannelSettingsViewModel>(navigationService, () => HasNext);
        SkipPipelineSettingsCommand = new SkipPipelineSettingsCommand(settings, navigationService);

        materialStore.StoreChanged += MaterialStore_StoreChanged;

        WeakReferenceMessenger.Default.Register<CreatePipeMessage>(this, OnPipeCreated);
        WeakReferenceMessenger.Default.Register<CreatePipeInsulationMessage>(this, OnPipeInsalutionCreated);
    }

    public string ViewTitle => WindowTitle;
    public PipelineSettingsModel Settings { get; }
    public ObservableCollection<RadialInsulation> PipeInsulationCollection { get; }
    public ObservableCollection<MaterialModel> PipeInsulationMaterials { get; }
    public ObservableCollection<MaterialModel> PipeMaterials { get; }
    public PipeInputDataViewModel InputPipeDataView { get; }
    public CreatePipeInsulationVewModel CreatePipeInsulationView { get; }

    public int ViewTitleFontSize
    {
        get => _viewTitleFontSize;
        set => Set(ref _viewTitleFontSize, value, nameof(ViewTitleFontSize));
    }
    public bool HasNext => Settings.Channel.Pipes.Count != 0;
    public bool CanCreatePipe => Settings.Channel.Pipes.Count < 4 && InputPipeDataView.IsValid;

    public ICommand CreatePipeCommand { get; }
    public ICommand RemovePipeCommand { get; }
    public ICommand RemovePipeInsulationLayerCommand { get; } 
    public ICommand NavigateNextCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public ICommand SkipPipelineSettingsCommand { get; }

    private void Pipes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CanCreatePipe));
        OnPropertyChanged(nameof(HasNext));
    }
    private void OnPipeInsalutionCreated(object recipient, CreatePipeInsulationMessage message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        PipeInsulationCollection.Add(message.Value);
    }
    private void OnPipeCreated(object recipient, CreatePipeMessage message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        Settings.Channel.Pipes.Add(message.Value);
        PipeInsulationCollection.Clear();
    }
    private void PipeSettingsViewModel_IsValidChanged()
    {
        OnPropertyChanged(nameof(CanCreatePipe));
        OnPropertyChanged(nameof(HasNext));
    }

    private void MaterialStore_StoreChanged()
    {
        PipeInsulationMaterials.UpdateCollection(_materialStore.GetInsulationMaterials());
        PipeMaterials.UpdateCollection(_materialStore.GetPipeMaterials());
    }
}
