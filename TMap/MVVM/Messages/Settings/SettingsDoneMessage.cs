namespace TMap.MVVM.Messages.Settings;

public class SettingsDoneMessage : ValueChangedMessage<object>
{
    public SettingsDoneMessage(object value) : base(value) { }
}
