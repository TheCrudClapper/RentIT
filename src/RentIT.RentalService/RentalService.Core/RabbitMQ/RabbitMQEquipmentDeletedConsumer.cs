using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalService.Core.RabbitMQ.Messages;
using RentalService.Core.ServiceContracts;
using System.Text;
using System.Text.Json;

namespace RentalService.Core.RabbitMQ;

public class RabbitMQEquipmentDeletedConsumer : IRabbitMQEquipmentDeletedConsumer, IDisposable
{

    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly IRentalService _rentalService;

    public RabbitMQEquipmentDeletedConsumer(IConfiguration configuration,
        IRentalService rentalService)
    {
        _rentalService = rentalService;
        _configuration = configuration;

        string hostName = _configuration["RABBITMQ_HOST_NAME"]!;
        string password = _configuration["RABBITMQ_PASSWORD"]!;
        string userName = _configuration["RABBITMQ_USER_NAME"]!;
        string port = _configuration["RABBITMQ_PORT"]!;

        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port),
            DispatchConsumersAsync = true
        };

        _connection = connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }

    public void Consume()
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

        _channel.QueueBind(queueName, exchangeName, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        //write code for the recieved message
        consumer.Received += async (sender, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            if(message != null)
            {
                EquipmentDeletedMessage? obj = JsonSerializer.Deserialize<EquipmentDeletedMessage>(message);
                await _rentalService.DeleteRentalByEquipmentId(obj.EquipmentId);
            }
            
        };

        _channel.BasicConsume(queue: queueName, consumer: consumer, autoAck: true);
    }
}
