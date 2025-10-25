using ReviewService.Core.DTO.Rental;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.Domain.HttpClientContracts;

public interface IRentalMicroserviceClient
{
    Task<Result<RentalResponse>> GetRentalByRentalIdAsync(Guid rentalId, CancellationToken cancellationToken = default);
}
