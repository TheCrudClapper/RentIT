using Polly;
namespace RentalService.Core.Policies.Contracts;

public interface IRentalMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();

    IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy();

    IAsyncPolicy<HttpResponseMessage> GetResiliencePolicy();
    
    IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy();

    IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerWithFallbackPolicy();
}

