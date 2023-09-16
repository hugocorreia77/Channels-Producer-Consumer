using Channels.Producer.Consumer.Core.Messages;
using System.Threading.Channels;

namespace Channels.Producer.Consumer.Core
{
    public class BikeProducer : BaseProducer<BikeMessage>
    {
        public BikeProducer(ChannelWriter<BikeMessage> channelWriter) : base(channelWriter)
        {
        }

        public async override Task ProduceAsync(BikeMessage message)
        {
            Console.WriteLine("Sending bike message...");
            await base.ProduceAsync(message);
        }

    }
}
