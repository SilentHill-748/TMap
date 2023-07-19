namespace TMap.MVVM.Messages.Settings.Map;

internal class MapLayerCreateMessage : ValueChangedMessage<Layer>
{
    public MapLayerCreateMessage(Layer value) : base(value)
    {
    }
}
