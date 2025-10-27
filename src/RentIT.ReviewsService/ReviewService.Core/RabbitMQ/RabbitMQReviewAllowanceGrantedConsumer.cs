using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReviewService.Core.DTO.ReviewAllowance;
using ReviewService.Core.RabbitMQ.Consumers.Base;
using ReviewService.Core.ServiceContracts;
using System.Text;
using System.Text.Json;

namespace ReviewService.Core.RabbitMQ;

internal class RabbitMQReviewAllowanceGrantedConsumer : RabbitMQBaseConsumer
{
    private readonly IReviewAllowanceService _reviewAllowanceService;
    public RabbitMQReviewAllowanceGrantedConsumer(IConfiguration configuration, IReviewAllowanceService reviewAllowanceService) : base(configuration)
    {
        _reviewAllowanceService = reviewAllowanceService;
    }

    private async Task Handle(ReviewAllowanceAddRequest? obj, CancellationToken cancellationToken) 
    {
        await _reviewAllowanceService.AddReviewAllowance(obj!, cancellationToken);
    }
     
    public override void Consume(CancellationToken cancellationToken)
    {
        string routingKey = "review.allowance.create";

        string queueName = "review.allowance.create.queue";

        string exchangeName = _configuration["RABBITMQ_RENTAL_EXCHANGE"]!;


        _channel.ExchangeDeclare(
            exchange: exchangeName,
            type: ExchangeType.Direct,
            durable: true);

        _channel.QueueDeclare(
            queue: queueName,
            exclusive: false,
            durable: true,
            autoDelete: false,
            arguments: null);

        _channel.QueueBind(
            queue: queueName,
            exchange: exchangeName,
            routingKey: routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (sender, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            if (message != null)
            {
                ReviewAllowanceAddRequest? obj = JsonSerializer
                    .Deserialize<ReviewAllowanceAddRequest>(message);

                await Handle(obj!, cancellationToken);
            }
        };

        _channel.BasicConsume(queue: queueName, consumer: consumer, autoAck: true);
    }
}
