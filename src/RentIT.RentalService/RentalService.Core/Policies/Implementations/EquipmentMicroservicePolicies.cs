using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.Policies.Contracts;
using System.Net;
using System.Text;
using System.Text.Json;

namespace RentalService.Core.Policies.Implementations
{
    public class EquipmentMicroservicePolicies : IEquipmentMicroservicePolicies
    {
        private readonly ILogger<EquipmentMicroservicePolicies> _logger;
        private readonly IPollyPolicies _pollyPolicies;
        public EquipmentMicroservicePolicies(IPollyPolicies pollyPolicies, ILogger<EquipmentMicroservicePolicies> logger)
        {
            _pollyPolicies = pollyPolicies;
            _logger = logger;
        }
        public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
        {
            var retry = _pollyPolicies.GetRetryPolicy(3);
            var cb = _pollyPolicies.GetCircuitBreakerPolicy(3, TimeSpan.FromMinutes(2));
            return Policy.WrapAsync(retry, cb);
        }

        public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicyForEquipmentsByIds()
        {
            var policy = Policy<HttpResponseMessage>
            .Handle<BrokenCircuitException>()
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

                 var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
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
    }
}
