using Polly;
using EquipmentService.Core.Policies.Contracts;
using Polly.Retry;
using Microsoft.Extensions.Logging;

namespace EquipmentService.Core.Policies.Implementations;

public class EquipmentServicePolicies : IEquipmentServicePolicies
{
    private readonly ILogger<EquipmentServicePolicies> _logger;
    public EquipmentServicePolicies(ILogger<EquipmentServicePolicies> logger)
    {
        _logger = logger;
    }
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        AsyncRetryPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(retryCount: 3,
        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(2),
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            _logger.LogInformation($"Retry {retryAttempt} after {timespan.TotalSeconds} secondss");
        });

        return policy;
    }
}

