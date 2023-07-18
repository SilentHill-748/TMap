namespace TMap.MVVM.Messages;

internal class MapLayerCreateMessage : ValueChangedMessage<Layer>
{
    public MapLayerCreateMessage(Layer value) : base(value)
    {
    }
}
