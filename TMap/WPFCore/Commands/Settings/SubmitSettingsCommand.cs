using System;

using CommunityToolkit.Mvvm.Messaging;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings;

public class SubmitSettingsCommand : CommandBase
{
    private readonly NavigationService _navigationService;
    private readonly PipelineChannelSettingsViewModel _viewModel;

    public SubmitSettingsCommand(
        PipelineChannelSettingsViewModel viewmodel,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));
        ArgumentNullException.ThrowIfNull(viewmodel, nameof(viewmodel));

        _navigationService = navigationService;
        _viewModel = viewmodel;
    }

    protected override void Execute()
    {
        _viewModel.Settings.PipelineSettings.IsSkiped = false;
        _viewModel.Settings.IsCompleted = true;

        _navigationService.NavigateTo<MapViewModel>();

        WeakReferenceMessenger.Default.Send(new SettingsDoneMessage(null!));
    }

    public override bool CanExecute()
    {
        return (!_viewModel.Settings.PipelineSettings.IsSkiped) && _viewModel.IsValid;
    }
}
