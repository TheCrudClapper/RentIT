using Polly;

namespace EquipmentService.Core.Policies.Contracts;

public interface IUsersMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
}

