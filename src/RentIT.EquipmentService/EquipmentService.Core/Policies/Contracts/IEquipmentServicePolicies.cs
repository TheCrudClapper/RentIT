using Polly;

namespace EquipmentService.Core.Policies.Contracts;

public interface IEquipmentServicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
}

