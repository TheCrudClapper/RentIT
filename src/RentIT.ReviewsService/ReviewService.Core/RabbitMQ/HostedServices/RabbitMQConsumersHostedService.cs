using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace ReviewService.Core.RabbitMQ.HostedServices;

public class RabbitMQConsumersHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private RabbitMQReviewAllowanceGrantedConsumer? _allowanceGrantedConsumer;
    public RabbitMQConsumersHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        _scopeFactory = serviceScopeFactory;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _scopeFactory.CreateScope();

        _allowanceGrantedConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQReviewAllowanceGrantedConsumer>();

        _allowanceGrantedConsumer.Consume(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _allowanceGrantedConsumer?.Dispose();
        return Task.CompletedTask;
    }
}
