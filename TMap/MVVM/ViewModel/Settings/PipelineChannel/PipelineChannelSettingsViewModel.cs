namespace TMap.MVVM.ViewModel.Settings.PipelineChannel;

public class PipelineChannelSettingsViewModel : ViewModelBase
{
    #region Dependencies
    private readonly MaterialStore _materialStore;
    #endregion

    #region Private fields
    private int _viewTitleFontSize;
    #endregion

    public PipelineChannelSettingsViewModel(
        SettingsModel settings,
        MaterialStore materialStore,
        NavigationService navigationService,
        CreateChannelInsulationValidator createChannelInsulationValidator,
        ChannelInputDataValidator channelInputDataValidator)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));
        ArgumentNullException.ThrowIfNull(createChannelInsulationValidator, nameof(createChannelInsulationValidator));
        ArgumentNullException.ThrowIfNull(channelInputDataValidator, nameof(channelInputDataValidator));

        _materialStore = materialStore;

        ChannelInsulationMaterials = new ObservableCollection<MaterialModel>(materialStore.GetChannelInsulationMaterials());
        PipeMaterials = new ObservableCollection<MaterialModel>(materialStore.GetPipeMaterials());

        WindowTitle = "Настройка коллектора трубопровода";
        ViewTitleFontSize = 22;

        var insulationLayers = settings.PipelineSettings.Channel.InsulationLayers;
        Settings = settings;
        InputChannelDataView = new ChannelInputDataViewModel(settings, channelInputDataValidator);
        CreateChannelInsulationView = new CreateChannelInsulationViewModel(ChannelInsulationMaterials, insulationLayers, createChannelInsulationValidator);

        NavigateBackCommand = new NavigateCommand<PipeSettingsViewModel>(navigationService);
        SubmitSettingsCommand = new SubmitSettingsCommand(this, navigationService);
        RemoveInsulationLayerCommand = new RemoveChannelInsulationLayerCommand(this);

        materialStore.StoreChanged += MaterialStore_StoreChanged;

        WeakReferenceMessenger.Default.Register<CreateChannelInsulationMessage>(this, OnInsulationCreated);
    }

    #region Public properties
    public string ViewTitle => WindowTitle;
    public SettingsModel Settings { get; }
    public ObservableCollection<MaterialModel> ChannelInsulationMaterials { get; }
    public ObservableCollection<MaterialModel> PipeMaterials { get; }
    public ChannelInputDataViewModel InputChannelDataView { get; }
    public CreateChannelInsulationViewModel CreateChannelInsulationView { get; }
    public override bool IsValid => InputChannelDataView.IsValid;
    #endregion

    #region Notify properties
    public int ViewTitleFontSize
    {
        get => _viewTitleFontSize;
        set => Set(ref _viewTitleFontSize, value, nameof(ViewTitleFontSize));
    }
    #endregion

    #region Commands
    public ICommand SubmitSettingsCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public ICommand RemoveInsulationLayerCommand { get; }
    #endregion

    #region Event handlers
    private void OnInsulationCreated(object recipient, CreateChannelInsulationMessage message)
    {
        Settings.PipelineSettings.Channel.InsulationLayers.Add(message.Value);
    }

    private void MaterialStore_StoreChanged()
    {
        ChannelInsulationMaterials.UpdateCollection(_materialStore.GetChannelInsulationMaterials());
        PipeMaterials.UpdateCollection(_materialStore.GetPipeMaterials());
    }
    #endregion
}
