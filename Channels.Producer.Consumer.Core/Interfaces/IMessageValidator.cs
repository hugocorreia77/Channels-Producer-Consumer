namespace Channels.Producer.Consumer.Core.Interfaces
{
    public interface IMessageValidator<T> where T : IMessage
    {
        bool Validate(T message);
    }
}
