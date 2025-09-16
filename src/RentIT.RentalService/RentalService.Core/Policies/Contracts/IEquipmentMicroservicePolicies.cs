using Polly;

namespace RentalService.Core.Policies.Contracts;

public interface IEquipmentMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicyForEquipmentsByIds();
}

