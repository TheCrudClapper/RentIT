using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalService.Core.Caching;
using RentalService.Core.RabbitMQ.Consumers.Base;
using RentalService.Core.RabbitMQ.Messages;
using RentalService.Core.ServiceContracts;
using System.Text;
using System.Text.Json;

namespace RentalService.Core.RabbitMQ.Consumers;

public class RabbitMQEquipmentDeletedConsumer : RabbitMQBaseConsumer
{
    private readonly IRentalService _rentalService;
    private readonly ICachingHelper _cachingHelper;
    private readonly ILogger<RabbitMQEquipmentDeletedConsumer> _logger;

    public RabbitMQEquipmentDeletedConsumer(
        IConfiguration configuration,
        IRentalService rentalService,
        ICachingHelper cachingHelper,
        ILogger<RabbitMQEquipmentDeletedConsumer> logger) : base(configuration)
    {
        _rentalService = rentalService;
        _cachingHelper = cachingHelper;
        _logger = logger;
    }

    private async Task HandleEquipmentDelete(Guid id)
    {
        _logger.LogInformation("Handling Equipment Delete");
        await _rentalService.DeleteRentalByEquipmentId(id);
        await _cachingHelper.InvalidateCache($"equipment:{id}");
    }

    public override void Consume()
    {
        //Routing / binding key 
        string routingKey = "equipment.delete";

        string queueName = "equipment.delete.queue";

        //Create exchange
        string exchangeName = _configuration["RABBITMQ_EQUIPMENT_EXCHANGE"]!;
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true);

        //Create message queue
        _channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        //Bind the message to exchange
        _channel.QueueBind(queueName, exchangeName, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        //write code for the recieved message
        consumer.Received += async (sender, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            if (message != null)
            {
                EquipmentDeletedMessage? obj = JsonSerializer.Deserialize<EquipmentDeletedMessage>(message);
                await HandleEquipmentDelete(obj!.EquipmentId);
            }

        };

        _channel.BasicConsume(queue: queueName, consumer: consumer, autoAck: true);
    }
}
