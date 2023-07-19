namespace TMap.MVVM.Messages.Settings.Road
{
    class CreateRoadLayerMessage : ValueChangedMessage<Layer>
    {
        public CreateRoadLayerMessage(Layer value) : base(value)
        {
        }
    }
}
