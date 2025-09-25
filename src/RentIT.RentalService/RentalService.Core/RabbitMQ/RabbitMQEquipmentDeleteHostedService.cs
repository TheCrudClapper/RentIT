using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RentalService.Core.RabbitMQ;

public class RabbitMQEquipmentDeleteHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private IRabbitMQEquipmentDeletedConsumer? _equipmentDeleteConsumer;
    public RabbitMQEquipmentDeleteHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _scopeFactory.CreateScope();
        _equipmentDeleteConsumer = scope.ServiceProvider.GetRequiredService<IRabbitMQEquipmentDeletedConsumer>();

        _equipmentDeleteConsumer.Consume();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _equipmentDeleteConsumer?.Dispose();

        return Task.CompletedTask;
    }
}
