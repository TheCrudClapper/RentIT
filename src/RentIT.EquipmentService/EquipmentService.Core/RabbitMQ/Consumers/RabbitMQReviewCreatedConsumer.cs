using EquipmentService.Core.RabbitMQ.Consumers.Base;
using EquipmentService.Core.RabbitMQ.Messages;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EquipmentService.Core.RabbitMQ.Consumers;

public class RabbitMQReviewCreatedConsumer : RabbitMQBaseConsumer
{
    private readonly IEquipmentService _equipmentService;
    public RabbitMQReviewCreatedConsumer(
        IConfiguration configuration,
        IEquipmentService equipmentService) : base(configuration)
    {
        _equipmentService = equipmentService;
    }

    public async Task Handle(ReviewCreated obj, CancellationToken cancellationToken)
    {
        await _equipmentService.UpdateEquipmentRating(obj.EquipmentId, obj.Rating, cancellationToken: cancellationToken);
    }

    public override void Consume(CancellationToken cancellationToken)
    {
        string routingKey = "review.created";

        string queueName = "review.created.queue";

        string exchangeName = _configuration["RABBITMQ_REVIEW_EXCHANGE"]!;

        _channel.ExchangeDeclare(
            exchange: exchangeName,
            type: ExchangeType.Direct,
            durable: true
            );

        _channel.QueueDeclare(
            queue: queueName,
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
                ReviewCreated? obj = JsonSerializer.Deserialize<ReviewCreated>(message);

                if(obj is not null)
                 await Handle(obj!, cancellationToken);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }
}
