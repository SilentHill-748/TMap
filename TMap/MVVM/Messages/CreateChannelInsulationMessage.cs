using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TMap.MVVM.Messages;

public class CreateChannelInsulationMessage : ValueChangedMessage<ChannelInsulation>
{
    public CreateChannelInsulationMessage(ChannelInsulation value) : base(value)
    {
    }
}
