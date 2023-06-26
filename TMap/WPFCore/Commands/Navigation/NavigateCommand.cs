using System;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Navigation;

public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationService _navigationService;

    public NavigateCommand(NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _navigationService = navigationService;
    }

    protected override void Execute()
    {
        _navigationService.NavigateTo<TViewModel>();
    }
}
