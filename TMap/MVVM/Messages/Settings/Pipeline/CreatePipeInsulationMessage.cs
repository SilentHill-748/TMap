namespace TMap.MVVM.Messages.Settings.Pipeline;

public class CreatePipeInsulationMessage : ValueChangedMessage<RadialInsulation>
{
    public CreatePipeInsulationMessage(RadialInsulation value) : base(value)
    {
    }
}
