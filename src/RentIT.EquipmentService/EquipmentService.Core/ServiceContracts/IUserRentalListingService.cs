using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.RentalListings;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.ServiceContracts;

/// <summary>
/// Defines operations for managing user-associated rental listings
/// </summary>
public interface IUserRentalListingService
{
    Task<Result<CreatedResponse>> AddUserRentalListing(Guid userId, RentalListingAddRequest request);
    Task<Result<UpdatedResponse>> UpdateUserRentalListing(Guid rentalListingId, Guid userId, RentalListingUpdateRequest request);
    Task<Result<RentalListingResponse>> GetUserRentalListingById(Guid rentalListingId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result> DeleteUserRentalListing(Guid rentalListingId, Guid userId);
    Task<Result<IReadOnlyCollection<RentalListingListResponse>>> GetAllUserRentalListings(Guid userId, CancellationToken cancellationToken = default);
}
