using RentIT.Contracts.DTO.Shared;
using ReviewService.Core.Domain.ResultTypes;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IRentalMicroserviceClient
{
    Task<Result<RentalResponse>> GetRentalByRentalIdAsync(Guid rentalId, CancellationToken cancellationToken = default);
}
