using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalService.Core.Caching;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.RabbitMQ.Consumers.Base;
using System.Text;
using System.Text.Json;

namespace RentalService.Core.RabbitMQ.Consumers;

public class RabbitMQEquipmentUpdateConsumer : RabbitMQBaseConsumer
{
    private readonly ICachingHelper _cachingHelper;
    public RabbitMQEquipmentUpdateConsumer(IConfiguration configuration, ICachingHelper cachingHelper) : base(configuration)
    {
        _cachingHelper = cachingHelper;
    }
    private async Task Handle(EquipmentResponse obj)
    {
        string cacheKey = CachingHelper.GenerateCacheKey("equipment", obj.Id);
        await _cachingHelper.CacheObject(obj, cacheKey, CachingProfiles.ShortTTLCacheOption);
    }
    public override void Consume()
    {
        string routingKey = "equipment.update";

        string queueName = "equipment.update.queue";

        string exchangeName = _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!;

        _channel.ExchangeDeclare(
            exchange: exchangeName,
            type: ExchangeType.Direct,
            durable: true
            );

        _channel.QueueDeclare(queue: queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

        _channel.QueueBind(queueName, exchangeName, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (sender, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            if (message != null)
            {
                EquipmentResponse? obj = JsonSerializer.Deserialize<EquipmentResponse>(message);
                await Handle(obj!);
            }
        };

        _channel.BasicConsume(queue: queueName, consumer: consumer, autoAck: true);
    }
}
