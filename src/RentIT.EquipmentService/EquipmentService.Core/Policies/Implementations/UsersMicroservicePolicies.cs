using Polly;
using EquipmentService.Core.Policies.Contracts;

namespace EquipmentService.Core.Policies.Implementations
{
    public class UsersMicroservicePolicies : IUsersMicroservicePolicies
    {
        private readonly IPollyPolicies _pollyPolicies;
        public UsersMicroservicePolicies(IPollyPolicies pollyPolicies)
        {
            _pollyPolicies = pollyPolicies;
        }
        public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
        {
            var cb = _pollyPolicies.GetCircuitBreakerPolicy(3, TimeSpan.FromMinutes(2));
            var retry = _pollyPolicies.GetRetryPolicy(3);
            return Policy.WrapAsync(retry, cb);
        }
    }
}
