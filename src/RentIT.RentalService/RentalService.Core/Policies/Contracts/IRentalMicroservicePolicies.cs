using Polly;
namespace RentalService.Core.Policies.Contracts;

public interface IRentalMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
}

