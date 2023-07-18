namespace TMap.MVVM.Messages
{
    class CreateRoadLayerMessage : ValueChangedMessage<Layer>
    {
        public CreateRoadLayerMessage(Layer value) : base(value)
        {
        }
    }
}
