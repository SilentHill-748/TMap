using System;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Navigation;

public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationService _navigationService;
    private readonly Func<bool>? _canExecute;

    public NavigateCommand(NavigationService navigationService, Func<bool>? canExecute = null)
    {
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _navigationService = navigationService;
        _canExecute = canExecute;
    }

    protected override void Execute()
    {
        _navigationService.NavigateTo<TViewModel>();
    }

    public override bool CanExecute()
    {
        return _canExecute is null || _canExecute();
    }
}
