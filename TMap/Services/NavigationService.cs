using Container = SimpleInjector.Container;

namespace TMap.Services;

public class NavigationService
{
    private readonly Container _container;

    public NavigationService(Container container)
    {
        _container = container;
    }

    public void NavigateTo<TViewModel>() 
        where TViewModel : ViewModelBase
    {
        TViewModel viewModel = _container.GetInstance<TViewModel>();

        var message = new NavigationChangedRequestedMessage(
            new NavigationModel() { DestinationViewModel = viewModel });

        WeakReferenceMessenger.Default.Send(message);
    }
}
