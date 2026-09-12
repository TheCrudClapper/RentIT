using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentalService.Core.RabbitMQ.Consumers;

namespace RentalService.Core.RabbitMQ.HostedServices;

public class RabbitMQConsumersHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private RabbitMQEquipmentDeletedConsumer? _equipmentDeleteConsumer;
    private RabbitMQEquipmentCreateConsumer? _equipmentCreateConsumer;
    private RabbitMQEquipmentUpdateConsumer? _equipmentUpdateConsumer;
    public RabbitMQConsumersHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _scopeFactory.CreateScope();

        _equipmentDeleteConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQEquipmentDeletedConsumer>();
        _equipmentCreateConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQEquipmentCreateConsumer>();
        _equipmentUpdateConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQEquipmentUpdateConsumer>();
        _equipmentDeleteConsumer.Consume(cancellationToken);
        _equipmentCreateConsumer.Consume(cancellationToken);
        _equipmentUpdateConsumer.Consume(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _equipmentDeleteConsumer?.Dispose();
        _equipmentCreateConsumer?.Dispose();
        _equipmentUpdateConsumer?.Dispose();
        return Task.CompletedTask;
    }
}
