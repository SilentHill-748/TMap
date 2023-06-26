using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TMap.MVVM.Messages
{
    class CreateRoadLayerMessage : ValueChangedMessage<RoadLayer>
    {
        public CreateRoadLayerMessage(RoadLayer value) : base(value)
        {
        }
    }
}
