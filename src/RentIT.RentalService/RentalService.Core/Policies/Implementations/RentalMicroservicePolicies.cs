using Microsoft.Extensions.Logging;
using Polly;
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
        AsyncRetryPolicy<HttpResponseMessage> policy =
        Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(retryCount: 3,
        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(2),
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            _logger.LogInformation($"Retry {retryAttempt}, after {timespan.TotalSeconds} seconds");
        });

        return policy;
    }
}

