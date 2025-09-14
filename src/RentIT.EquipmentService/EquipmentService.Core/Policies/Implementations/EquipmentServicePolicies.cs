using Polly;
using EquipmentService.Core.Policies.Contracts;
using Polly.Retry;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;

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
        var random = new Random();
        AsyncRetryPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: retryAttempt => 
        {
            double baseDelay = Math.Pow(2, retryAttempt);
            double jitter = random.NextDouble();
            return TimeSpan.FromSeconds(baseDelay + jitter);
        },
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            _logger.LogInformation($"Retry {retryAttempt} after {timespan.TotalSeconds} secondss");
        });

        return policy;
    }
    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
       AsyncCircuitBreakerPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 3,
            durationOfBreak: TimeSpan.FromMinutes(2),
            onBreak: (outcome, timespan) =>
            {
                _logger.LogInformation($"Circuit breaker opened for {timespan.TotalMinutes} minutes due to consecutive 3 failures." +
               $" Subsequent requests will be blocked");
            },
            onReset: () =>
            {
                _logger.LogInformation($"Circut breaker closed, request will " +
                $"flow now");
            });

        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetResiliencePolicy()
    {
        var retryPolicy = GetRetryPolicy();

        var circuitBreakerPolicy = GetCircuitBreakerPolicy();

        return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
    }
}

