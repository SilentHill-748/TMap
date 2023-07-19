namespace TMap.MVVM.Messages.Navigation;

internal class NavigationChangedRequestedMessage : ValueChangedMessage<NavigationModel>
{
    public NavigationChangedRequestedMessage(NavigationModel value)
        : base(value) { }
}
