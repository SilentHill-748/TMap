using System;

using CommunityToolkit.Mvvm.Messaging;

namespace TMap.MVVM.ViewModel;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _currentViewModel;
    private readonly MapViewModel _defaultView;

    private double _minWidth;
    private double _minHeight;
    private int _fontSize;

    public MainViewModel(MapViewModel defaultViewModel)
    {
        ArgumentNullException.ThrowIfNull(defaultViewModel, nameof(defaultViewModel));

        _defaultView = defaultViewModel;
        MinWidth = 1200;
        MinHeight = 500;
        FontSize = 16;

        WeakReferenceMessenger.Default.Register<NavigationChangedRequestedMessage>(this, NavigateTo);
    }

    public int FontSize
    {
        get => _fontSize;
        set => Set(ref _fontSize, value, nameof(FontSize));
    }
    public double MinWidth
    {
        get => _minWidth;
        set => Set(ref _minWidth, value, nameof(MinWidth));
    }
    public double MinHeight
    {
        get => _minHeight;
        set => Set(ref _minHeight, value, nameof(MinHeight));
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
