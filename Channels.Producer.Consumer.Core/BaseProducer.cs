using Channels.Producer.Consumer.Core.Interfaces;
using System.Threading.Channels;

namespace Channels.Producer.Consumer.Core
{
    public abstract class BaseProducer<T> : IProducer<T> where T : IMessage
    {
        private readonly ChannelWriter<T> _channelWriter;

        public BaseProducer(ChannelWriter<T> channelWriter) {
            _channelWriter = channelWriter;
        }

        public async virtual Task ProduceAsync(T message)
        {
            await _channelWriter.WriteAsync(message);
            Console.WriteLine("Message sent.");
        }
    }
}
