using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Messaging;

using TMap.Configurations.Extentions;
using TMap.MVVM.Stores;

namespace TMap.MVVM.ViewModel.Settings;

public class PipelineChannelSettingsViewModel : ViewModelBase
{
    private readonly MaterialStore _materialStore;

    private int _viewTitleFontSize;

    public PipelineChannelSettingsViewModel(
        SettingsModel settings,
        MaterialStore materialStore,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _materialStore = materialStore;

        ChannelInsulationMaterials = new ObservableCollection<MaterialModel>(materialStore.GetChannelInsulationMaterials());
        PipeMaterials = new ObservableCollection<MaterialModel>(materialStore.GetPipeMaterials());

        WindowTitle = "Настройка коллектора трубопровода";
        ViewTitleFontSize = 22;

        Settings = settings;
        InputChannelDataView = new InputChannelDataViewModel(settings);
        CreateChannelInsulationView = new CreateChannelInsulationViewModel(ChannelInsulationMaterials, Settings.PipelineSettings.Channel.InsulationLayers);

        NavigateBackCommand = new NavigateCommand<PipeSettingsViewModel>(navigationService);
        SubmitSettingsCommand = new SubmitSettingsCommand(this, navigationService);
        RemoveInsulationLayerCommand = new RemoveChannelInsulationLayerCommand(this);

        materialStore.StoreChanged += MaterialStore_StoreChanged;

        WeakReferenceMessenger.Default.Register<CreateChannelInsulationMessage>(this, OnInsulationCreated);
    }

    public string ViewTitle => WindowTitle;
    public SettingsModel Settings { get; }
    public ObservableCollection<MaterialModel> ChannelInsulationMaterials { get; }
    public ObservableCollection<MaterialModel> PipeMaterials { get; }
    public InputChannelDataViewModel InputChannelDataView { get; }
    public CreateChannelInsulationViewModel CreateChannelInsulationView { get; }
    public int ViewTitleFontSize
    {
        get => _viewTitleFontSize;
        set => Set(ref _viewTitleFontSize, value, nameof(ViewTitleFontSize));
    }

    public override bool IsValid => InputChannelDataView.IsValid;

    public ICommand SubmitSettingsCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public ICommand RemoveInsulationLayerCommand { get; }

    private void OnInsulationCreated(object recipient, CreateChannelInsulationMessage message)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        Settings.PipelineSettings.Channel.InsulationLayers.Add(message.Value);
    }

    private void MaterialStore_StoreChanged()
    {
        ChannelInsulationMaterials.UpdateCollection(_materialStore.GetChannelInsulationMaterials());
        PipeMaterials.UpdateCollection(_materialStore.GetPipeMaterials());
    }
}
