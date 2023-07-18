using System;

using CommunityToolkit.Mvvm.Messaging;

namespace TMap.MVVM.ViewModel;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _currentViewModel;
    private readonly MapViewModel _defaultView;

    public MainViewModel(MapViewModel defaultViewModel)
    {
        ArgumentNullException.ThrowIfNull(defaultViewModel, nameof(defaultViewModel));

        _defaultView = defaultViewModel;

        WeakReferenceMessenger.Default.Register<NavigationChangedRequestedMessage>(this, NavigateTo);
    }

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel ?? _defaultView;
        set => Set(ref _currentViewModel, value, nameof(CurrentViewModel));
    }

    private void NavigateTo(object recipient, NavigationChangedRequestedMessage message)
    {
        if (message.Value is NavigationModel navigation)
        {
            CurrentViewModel = navigation.DestinationViewModel ?? _defaultView;
            WindowTitle = CurrentViewModel.WindowTitle;
        }
    }
}
