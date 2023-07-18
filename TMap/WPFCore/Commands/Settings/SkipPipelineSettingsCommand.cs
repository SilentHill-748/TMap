namespace TMap.WPFCore.Commands.Settings;

public class SkipPipelineSettingsCommand : CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly SettingsModel _settings;

    public SkipPipelineSettingsCommand(SettingsModel settings, NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _settings = settings;
        _navigationService = navigationService;
    }

    protected override void Execute()
    {
        _settings.PipelineSettings.IsSkiped = true;
        _settings.PipelineSettings.Channel.Clear();
        _settings.IsCompleted = true;

        _navigationService.NavigateTo<MapViewModel>();

        WeakReferenceMessenger.Default.Send(new SettingsDoneMessage(null!));
    }
}
