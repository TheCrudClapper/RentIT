using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RentalService.Core.Policies.Contracts;

namespace RentalService.Core.Policies.Implementations;

public class PollyPolicies : IPollyPolicies
{
    private readonly ILogger<PollyPolicies> _logger;
    public PollyPolicies(ILogger<PollyPolicies> logger)
    {
        _logger = logger;
    }
    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int eventsAllowedBeforeBreaking, TimeSpan breakDuration)
    {
        AsyncCircuitBreakerPolicy<HttpResponseMessage> policy =
            Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: eventsAllowedBeforeBreaking,
                    durationOfBreak: breakDuration,
                    onBreak: (outcome, timespan) =>
                    {
                        _logger.LogInformation(
                            $"Circuit breaker opened for {timespan.TotalMinutes} minutes due to consecutive 3 failures." +
                            $" Subsequent requests will be blocked");
                    },
                    onReset: () => { _logger.LogInformation($"Circuit breaker closed, request will flow now"); });
        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount)
    {
        var random = new Random();

        AsyncRetryPolicy<HttpResponseMessage> policy =
            Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    retryCount: retryCount,
                    sleepDurationProvider: retryAttempt =>
                    {
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

    public IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(TimeSpan timeout)
    {
        return Policy.TimeoutAsync<HttpResponseMessage>(timeout);
    }
}

