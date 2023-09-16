namespace Channels.Producer.Consumer.Core.Interfaces
{
    public interface IProducer<T> 
    {
        Task ProduceAsync(T message);
    }
}
