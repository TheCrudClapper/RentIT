using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ResultTypes;

namespace RentalService.Core.ServiceContracts;
/// <summary>
/// Defines operations for managing equipment rentals associated with a specific user, including retrieval, creation,
/// update, deletion, and marking equipment as returned.
/// </summary>
/// <remarks>This interface provides asynchronous methods for handling user-specific rental workflows. All
/// operations require a valid user identifier to ensure that actions are performed within the context of the correct
/// user. Methods return a Result type to indicate success or failure and may include additional data or error
/// information. Implementations should ensure appropriate authorization and validation for each operation.</remarks>
public interface IUserRentalService
{
    Task<Result<IEnumerable<RentalResponse>>> GetAllRentals(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<RentalResponse>> GetRental(Guid rentalId, Guid userId, CancellationToken cancellationToken = default );
    Task<Result<RentalResponse>> AddRental(UserRentalAddRequest request, Guid userId, CancellationToken cancellationToken = default);
    Task<Result> UpdateRental(Guid rentalId, UserRentalUpdateRequest request, Guid userId, CancellationToken cancellationToken = default);
    Task<Result> DeleteRental(Guid rentalId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result> MarkEquipmentAsReturned(Guid rentalId, Guid userId, UserReturnEquipmentRequest request, CancellationToken cancellationToken = default);
}
