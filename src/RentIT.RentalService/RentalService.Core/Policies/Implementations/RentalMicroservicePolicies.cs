using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RentalService.Core.Policies.Contracts;
namespace RentalService.Core.Policies.Implementations;

public class RentalMicroservicePolicies : IRentalMicroservicePolicies
{
    private readonly ILogger<RentalMicroservicePolicies> _logger;
    public RentalMicroservicePolicies(ILogger<RentalMicroservicePolicies> logger)
    {
        _logger = logger;
    }


    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var random = new Random();

        AsyncRetryPolicy<HttpResponseMessage> policy =
        Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: retryAttempt => {

            double baseDelay = Math.Pow(2, retryAttempt);
            double jitter = random.NextDouble();
            return TimeSpan.FromSeconds(baseDelay + jitter);
        },
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            _logger.LogInformation($"Retry {retryAttempt}, after {timespan.TotalSeconds} seconds");
        });

        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        AsyncCircuitBreakerPolicy<HttpResponseMessage> policy =
        Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .CircuitBreakerAsync(
        //after 3 consecutive requests the braker will be opened
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

