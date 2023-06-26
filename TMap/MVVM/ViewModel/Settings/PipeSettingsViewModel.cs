using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Messaging;

namespace TMap.MVVM.ViewModel.Settings;

public class PipeSettingsViewModel : ViewModelBase
{
    private int _viewTitleFontSize;

    public PipeSettingsViewModel(SettingsModel settings, NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        Settings = settings.PipelineSettings;
        WindowTitle = "Настройка труб для коллектора трубопровода";
        ViewTitleFontSize = 22;
        PipeInsulationCollection = new ObservableCollection<RadialInsulation>();

        InputPipeDataView = new InputPipeDataViewModel(Settings.PipeMaterials);
        CreatePipeInsulationView = new CreatePipeInsulationVewModel(Settings.PipeInsulationMaterials, PipeInsulationCollection);

        Settings.Channel.Pipes.CollectionChanged += Pipes_CollectionChanged;
        InputPipeDataView.IsValidChanged += PipeSettingsViewModel_IsValidChanged;

        CreatePipeCommand = new CreatePipeCommand(this);
        RemovePipeCommand = new RemovePipeCommand(this);
        RemovePipeInsulationLayerCommand = new RemovePipeInsulationLayerCommand(this);
        NavigateBackCommand = new NavigateCommand<RoadSettingsViewModel>(navigationService);
        NavigateNextCommand = new NavigateCommand<PipelineChannelSettingsViewModel>(navigationService);
        SkipPipelineSettingsCommand = new SkipPipelineSettingsCommand(settings, navigationService);

        WeakReferenceMessenger.Default.Register<CreatePipeMessage>(this, OnPipeCreated);
        WeakReferenceMessenger.Default.Register<CreatePipeInsulationMessage>(this, OnPipeInsalutionCreated);
    }

    public string ViewTitle => WindowTitle;
    public PipelineSettingsModel Settings { get; }
    public ObservableCollection<RadialInsulation> PipeInsulationCollection { get; }
    public InputPipeDataViewModel InputPipeDataView { get; }
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
    }
}
