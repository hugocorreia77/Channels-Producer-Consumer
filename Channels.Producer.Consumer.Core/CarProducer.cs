using Channels.Producer.Consumer.Core.Messages;
using System.Threading.Channels;

namespace Channels.Producer.Consumer.Core
{
    public class CarProducer : BaseProducer<CarMessage>
    {
        public CarProducer(ChannelWriter<CarMessage> channelWriter) : base(channelWriter)
        {
        }

        public async override Task ProduceAsync(CarMessage message)
        {
            Console.WriteLine("Sending car message...");
            await base.ProduceAsync(message);
        }

    }
}
