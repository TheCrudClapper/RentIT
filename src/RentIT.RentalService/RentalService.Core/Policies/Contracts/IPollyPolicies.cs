using Polly;
namespace RentalService.Core.Policies.Contracts;

public interface IPollyPolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount);
    IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int eventsAllowedBeforeBreaking, TimeSpan breakDuration);
    IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(TimeSpan timeout);

}
