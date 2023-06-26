using System;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Messaging;

namespace TMap.MVVM.ViewModel.Settings;

public class PipelineChannelSettingsViewModel : ViewModelBase
{
    private int _viewTitleFontSize;

    public PipelineChannelSettingsViewModel(
        SettingsModel settings,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        WindowTitle = "Настройка коллектора трубопровода";
        ViewTitleFontSize = 22;

        Settings = settings;
        InputChannelDataView = new InputChannelDataViewModel(settings.RoadSettings, Settings.PipelineSettings);
        CreateChannelInsulationView = new CreateChannelInsulationViewModel(Settings.PipelineSettings.ChannelInsulationMaterials, Settings.PipelineSettings.Channel.InsulationLayers);

        NavigateBackCommand = new NavigateCommand<PipeSettingsViewModel>(navigationService);
        SubmitSettingsCommand = new SubmitSettingsCommand(this, navigationService);
        RemoveInsulationLayerCommand = new RemoveChannelInsulationLayerCommand(this);

        WeakReferenceMessenger.Default.Register<CreateChannelInsulationMessage>(this, OnInsulationCreated);
    }

    public string ViewTitle => WindowTitle;
    public SettingsModel Settings { get; }
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
}
