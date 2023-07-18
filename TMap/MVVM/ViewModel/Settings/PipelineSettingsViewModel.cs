using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Messaging;

using TMap.MVVM.Stores;

namespace TMap.MVVM.ViewModel.Settings;

[Obsolete("На удаление.")]
public class PipelineSettingsViewModel : ViewModelBase
{
    private int _titleFontSize;

    public PipelineSettingsViewModel(
        SettingsModel settings,
        MaterialStore materialStore,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        Settings = settings.PipelineSettings;
        WindowTitle = "Настройка параметров трубопровода";
        _titleFontSize = 22;

        PipelineChannelView = new PipelineChannelSettingsViewModel(settings, materialStore, navigationService);
        CreatePipeView = new PipeSettingsViewModel(settings, materialStore, navigationService);

        ExitFromSettingsCommand = new NavigateCommand<MapViewModel>(navigationService);

        WeakReferenceMessenger.Default.Register<CreatePipeMessage>(this, OnPipeCreated);
    }

    public PipelineSettingsModel Settings { get; }
    public ObservableCollection<Pipe> PipeCollection => Settings.Channel.Pipes;
    public PipelineChannelSettingsViewModel PipelineChannelView { get; }
    public PipeSettingsViewModel CreatePipeView { get; }

    public string ViewTitle => WindowTitle;
    public int TitleFontSize
    {
        get => _titleFontSize;
        set => Set(ref _titleFontSize, value, nameof(TitleFontSize));
    }

    public ICommand ExitFromSettingsCommand { get; }

    private void OnPipeCreated(object recipient, CreatePipeMessage message)
    {
        if (message is not { })
            throw new Exception("Ошибка создания трубы!");

        PipeCollection.Add(message.Value);
    }
}
