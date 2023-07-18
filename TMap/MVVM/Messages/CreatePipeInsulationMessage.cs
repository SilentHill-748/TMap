namespace TMap.MVVM.Messages;

public class CreatePipeInsulationMessage : ValueChangedMessage<RadialInsulation>
{
    public CreatePipeInsulationMessage(RadialInsulation value) : base(value)
    {
    }
}
