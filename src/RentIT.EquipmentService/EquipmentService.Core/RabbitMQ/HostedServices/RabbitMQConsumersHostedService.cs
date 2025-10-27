using EquipmentService.Core.RabbitMQ.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EquipmentService.Core.RabbitMQ.HostedServices;

public class RabbitMQConsumersHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private RabbitMQReviewCreatedConsumer? _reviewCreatedConsumer;
    private RabbitMQReviewDeletedConsumer? _reviewDeletedConsumer;
    private RabbitMQReviewUpdatedConsumer? _reviewUpdatedConsumer;   
    public RabbitMQConsumersHostedService(
        IServiceScopeFactory serviceScopeFactory)
    {
        _scopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _scopeFactory.CreateScope();
        
        _reviewCreatedConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQReviewCreatedConsumer>();
        _reviewDeletedConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQReviewDeletedConsumer>();
        _reviewUpdatedConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQReviewUpdatedConsumer>();

        _reviewCreatedConsumer.Consume(cancellationToken);
        _reviewDeletedConsumer.Consume(cancellationToken);
        _reviewUpdatedConsumer.Consume(cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _reviewCreatedConsumer?.Dispose();
        _reviewDeletedConsumer?.Dispose();
        _reviewUpdatedConsumer?.Dispose();

        return Task.CompletedTask;
    }
}
