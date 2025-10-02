using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace RentalService.Core.RabbitMQ.Consumers.Base;

/// <summary>
/// Provides a base implementation for a RabbitMQ consumer that manages connection and channel lifecycle, and defines
/// the contract for consuming messages.
/// </summary>
/// <remarks>This abstract class handles the initialization of the RabbitMQ connection and channel using
/// configuration settings. Derived classes should implement the <see cref="Consume"/> method to define specific message
/// consumption logic. The class implements <see cref="IDisposable"/> to ensure proper resource cleanup. Thread safety
/// is not guaranteed; if multiple threads access an instance concurrently, external synchronization is
/// required.</remarks>
public abstract class RabbitMQBaseConsumer : IRabbitMQBaseConsumer, IDisposable
{
    protected readonly IConnection _connection;
    protected readonly IModel _channel;
    protected readonly IConfiguration _configuration;

    protected RabbitMQBaseConsumer(IConfiguration configuration)
    {
        _configuration = configuration;

        //Read env variables
        string hostName = _configuration["RABBITMQ_HOST_NAME"]!;
        string password = _configuration["RABBITMQ_PASSWORD"]!;
        string userName = _configuration["RABBITMQ_USER_NAME"]!;
        string port = _configuration["RABBITMQ_PORT"]!;

        ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port),
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();
    }

    public abstract void Consume(CancellationToken cancellationToken);
    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
