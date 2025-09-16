using EquipmentService.Core.Policies.Contracts;
using Polly;

namespace EquipmentService.Core.Policies.Implementations;

public class RentalMicroservicePolicies : IRentalMicroservicePolicies
{
    private readonly IPollyPolicies _pollyPolicies;
    public RentalMicroservicePolicies(IPollyPolicies pollyPolicies)
    {
        _pollyPolicies = pollyPolicies;
    }
    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
    {
        var cb = _pollyPolicies.GetCircuitBreakerPolicy(3, TimeSpan.FromMinutes(2));
        var retryPolicy = _pollyPolicies.GetRetryPolicy(3);

        return Policy.WrapAsync(retryPolicy, cb);
    }
}

