using Channels.Producer.Consumer.Core.Interfaces;
using System.Text.Json;
using System.Threading.Channels;

namespace Channels.Producer.Consumer.Core.Consumers
{
    public abstract class BaseConsumer<T> : IConsumer where T : IMessage
    {
        private readonly ChannelReader<T> _channelReader;
        private readonly CancellationToken _cancelationToken;
        private readonly CancellationTokenSource _cancelationTokenSource;
        private readonly IMessageValidator<T> _validator;

        public abstract string _name { get; }

        public BaseConsumer(ChannelReader<T> channel, IMessageValidator<T> validator)
        {
            _channelReader = channel;
            _cancelationTokenSource = new CancellationTokenSource();
            _cancelationToken = _cancelationTokenSource.Token;
            _validator = validator;
        }

        public async Task StartConsumingAsync()
        {
            Console.WriteLine($"Consumer {_name} started!");
            
            while (!_cancelationToken.IsCancellationRequested)
            {
                var message = await _channelReader.ReadAsync();
                Console.WriteLine($"Message received: {JsonSerializer.Serialize(message)}");

                
                if (!_validator.Validate(message))
                {
                    Console.WriteLine("Message is not in a correct format: ");
                }

                await HandleMessage(message);
            }
        }

        public Task StopConsuming()
        {
            _cancelationTokenSource.Cancel();
            Console.WriteLine($"Consumer {_name} stoped...");
            return Task.CompletedTask;
        }

        public abstract Task HandleMessage(T message);

    }
}
