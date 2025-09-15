using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RentalService.Core.DTO.RentalDto;
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
                        _logger.LogInformation(
                            $"Circuit breaker opened for {timespan.TotalMinutes} minutes due to consecutive 3 failures." +
                            $" Subsequent requests will be blocked");
                    },
                    onReset: () => { _logger.LogInformation($"Circuit breaker closed, request will flow now"); });
        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetResiliencePolicy()
    {
        var retryPolicy = GetRetryPolicy();

        var circuitBreakerPolicy = GetCircuitBreakerPolicy();

        return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
    }

    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    {
        var policy = Policy<HttpResponseMessage>
            .Handle<BrokenCircuitException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .FallbackAsync(
             async (outcome, context, ct) =>
             {

                 var dummyList = new List<EquipmentResponse>();
                 if (context.TryGetValue("equipmentIds", out var idsObj) && idsObj is IEnumerable<Guid> ids)
                 {
                     dummyList = ids.Select(id =>
                         new EquipmentResponse(id, "Unknown", Guid.Empty, 0, "Unknown", "Unknown", "Unknown", "Unknown")
                     ).ToList();
                 }

                 var response = new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new StringContent(JsonSerializer.Serialize(dummyList), Encoding.UTF8, "application/json")
                 };

                 return await Task.FromResult(response);
             },
             async (outcome, context) =>
             {
                 _logger.LogInformation("Fallback triggered: returning dummy data");
                 await Task.CompletedTask;
             });

        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerWithFallbackPolicy()
    {
        var circuitBreakerPolicy = GetCircuitBreakerPolicy();
        var fallbackPolicy = GetFallbackPolicy();
        return Policy.WrapAsync(fallbackPolicy, circuitBreakerPolicy);
    }
}