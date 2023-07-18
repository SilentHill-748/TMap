namespace TMap.MVVM.Messages;

internal class NavigationChangedRequestedMessage : ValueChangedMessage<NavigationModel>
{
    public NavigationChangedRequestedMessage(NavigationModel value) 
        : base(value) { }
}
