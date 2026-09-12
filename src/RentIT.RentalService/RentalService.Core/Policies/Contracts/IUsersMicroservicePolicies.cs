using Polly;

namespace RentalService.Core.Policies.Contracts;

public interface IUsersMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
}

