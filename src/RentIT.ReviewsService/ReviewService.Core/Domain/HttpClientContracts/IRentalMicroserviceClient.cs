using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.Rentals;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IRentalMicroserviceClient
{
    Task<Result<RentalResponse>> GetRentalByRentalIdAsync(Guid rentalId, CancellationToken cancellationToken = default);
}
