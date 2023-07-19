namespace TMap.MVVM.Messages.Settings.PipelineChannel;

public class CreateChannelInsulationMessage : ValueChangedMessage<ChannelInsulation>
{
    public CreateChannelInsulationMessage(ChannelInsulation value) : base(value)
    {
    }
}
