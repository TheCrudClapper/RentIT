using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts;

public interface IUserRentalService
{
    Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(Guid userId, CancellationToken cancellationToken);
    Task<Result<RentalResponse>> GetRental(Guid rentalId, Guid userId, CancellationToken cancellationToken);
    Task<Result<RentalResponse>> AddRental(UserRentalAddRequest request, Guid userId, CancellationToken cancellationToken);
    Task<Result> UpdateRental(Guid rentalId, UserRentalUpdateRequest request, Guid userId, CancellationToken cancellationToken);
    Task<Result> DeleteRental(Guid rentalId, Guid userId, CancellationToken cancellationToken);
}
