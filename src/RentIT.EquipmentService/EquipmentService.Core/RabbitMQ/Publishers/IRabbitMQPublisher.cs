namespace EquipmentService.Core.RabbitMQ.Publishers;

public interface IRabbitMQPublisher
{
    void Publish<T>(string routingKey, T message, string exchangeName);
}
