namespace Channels.Producer.Consumer.Core.Interfaces
{
    public interface IConsumer
    {
        Task StartConsumingAsync();
        Task StopConsuming();
    }
}
