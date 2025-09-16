using Polly;

namespace EquipmentService.Core.Policies.Contracts
{
    public interface IRentalMicroservicePolicies
    {
        public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
    }
}
