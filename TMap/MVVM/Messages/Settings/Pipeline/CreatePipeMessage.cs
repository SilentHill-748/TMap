namespace TMap.MVVM.Messages.Settings.Pipeline;

public class CreatePipeMessage : ValueChangedMessage<Pipe>
{
    public CreatePipeMessage(Pipe value) : base(value)
    {
    }
}
