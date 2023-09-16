using Channels.Producer.Consumer.Core.Interfaces;
using Channels.Producer.Consumer.Core.Messages;
using System.Text.Json;
using System.Threading.Channels;

namespace Channels.Producer.Consumer.Core.Consumers.Cars
{
    public class CarConsumer<T> : BaseConsumer<T> where T : CarMessage
    {
        public CarConsumer(ChannelReader<T> channel, IMessageValidator<T> validator) : base(channel, validator) { }

        public override string _name { get => typeof(CarConsumer<T>).ToString(); }

        public override Task HandleMessage(T message)
        {
            Console.WriteLine("Handling the message....");

            // inject a service to call a repo to save the bike into the DB
            //... _bikeService.Save(message);
            Console.WriteLine($"The car {JsonSerializer.Serialize(message)} was stored in the DB.");

            Console.WriteLine();
            return Task.CompletedTask;
        }
    }
}
