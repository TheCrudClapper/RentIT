namespace RentalService.Core.RabbitMQ.Consumers.Base;

public interface IRabbitMQBaseConsumer
{
    /// <summary>
    /// Performs an consumption of message sent by publisher
    /// </summary>
    void Consume(CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes resources used in communication
    /// </summary>
    void Dispose();
}
