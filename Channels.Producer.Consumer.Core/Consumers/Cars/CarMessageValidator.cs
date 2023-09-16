using Channels.Producer.Consumer.Core.Interfaces;
using Channels.Producer.Consumer.Core.Messages;

namespace Channels.Producer.Consumer.Core.Consumers.Cars
{
    public class CarMessageValidator<T> : IMessageValidator<T> where T : CarMessage
    {
        public bool Validate(T message)
        {
            if (message == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(message.Description))
            {
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
