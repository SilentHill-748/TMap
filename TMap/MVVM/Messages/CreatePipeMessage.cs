namespace TMap.MVVM.Messages;

public class CreatePipeMessage : ValueChangedMessage<Pipe>
{
    public CreatePipeMessage(Pipe value) : base(value)
    {
    }
}
