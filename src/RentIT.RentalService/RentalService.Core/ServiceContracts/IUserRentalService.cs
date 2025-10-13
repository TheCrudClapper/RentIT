using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts;

public interface IUserRentalService
{
    Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<RentalResponse>> GetRental(Guid rentalId, Guid userId, CancellationToken cancellationToken = default );
    Task<Result<RentalResponse>> AddRental(UserRentalAddRequest request, Guid userId, string accessToken, CancellationToken cancellationToken = default);
    Task<Result> UpdateRental(Guid rentalId, UserRentalUpdateRequest request, Guid userId, string accessToken, CancellationToken cancellationToken = default);
    Task<Result> DeleteRental(Guid rentalId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result> MarkEquipmentAsReturned(Guid rentalId, Guid userId, UserReturnEquipmentRequest request, CancellationToken cancellationToken = default);
}
