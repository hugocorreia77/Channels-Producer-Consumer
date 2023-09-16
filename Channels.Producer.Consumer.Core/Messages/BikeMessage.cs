namespace Channels.Producer.Consumer.Core.Messages
{
    public class BikeMessage : BaseMessage
    {
        public int Size { get; set; }
        public string Name { get; set; }
    }
}
