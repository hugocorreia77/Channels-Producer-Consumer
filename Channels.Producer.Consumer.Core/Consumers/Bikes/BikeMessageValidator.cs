using Channels.Producer.Consumer.Core.Interfaces;
using Channels.Producer.Consumer.Core.Messages;

namespace Channels.Producer.Consumer.Core.Consumers.Bikes
{
    public class BikeMessageValidator<T> : IMessageValidator<T> where T : BikeMessage
    {
        public bool Validate(T message)
        {
            if(message == null)
            {
                return false;
            }

            if(message.Size < 0) {
                return false;
            }

            if (string.IsNullOrEmpty(message.Name))
            {
                return false;
            }

            return true;
        }
    }
}
