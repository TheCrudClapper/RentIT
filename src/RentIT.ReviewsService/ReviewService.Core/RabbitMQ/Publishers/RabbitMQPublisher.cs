using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using ReviewServices.Core.RabbitMQ.Publishers;
using System.Text;
using System.Text.Json;

namespace ReviewService.Core.RabbitMQ.Publishers;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMQPublisher(IConfiguration configuration)
    {
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
        };

        _connection =  connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }

    public void Publish<T>(string routingKey, T message, string exchangeName)
    {
        string messageJson = JsonSerializer.Serialize(message);
        byte[] bytes = Encoding.UTF8.GetBytes(messageJson);

        //Create an exchange
        _channel.ExchangeDeclare(
            exchange: exchangeName,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false
            );

        //Publish Message
        _channel.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: null,
            body: bytes);

        //basic properties is for headers
    }
}
