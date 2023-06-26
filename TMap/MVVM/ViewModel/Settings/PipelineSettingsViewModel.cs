using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Messaging;

namespace TMap.MVVM.ViewModel.Settings;

public class PipelineSettingsViewModel : ViewModelBase
{
    private int _titleFontSize;

    public PipelineSettingsViewModel(
        SettingsModel settings,
        NavigationService navigationService)
    {
        Settings = settings.PipelineSettings;
        WindowTitle = "Настройка параметров трубопровода";
        _titleFontSize = 22;

        PipelineChannelView = new PipelineChannelSettingsViewModel(settings, navigationService);
        CreatePipeView = new PipeSettingsViewModel(settings, navigationService);

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
