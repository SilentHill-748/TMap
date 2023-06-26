using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TMap.MVVM.Messages;

public class SettingsDoneMessage : ValueChangedMessage<object>
{
    public SettingsDoneMessage(object value) : base(value) { }
}
