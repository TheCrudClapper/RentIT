using EquipmentService.Core.Domain.ResultTypes;
using RentIT.Contracts.DTO.EquipmentMicroservice.RentalListings;
using RentIT.Contracts.DTO.Shared;

namespace EquipmentService.Core.ServiceContracts;

/// <summary>
/// Defines operations for managing user-associated rental listings
/// </summary>
public interface IUserRentalListingService
{
    Task<Result<CreatedResponse>> AddUserRentalListing(Guid userId, RentalListingAddRequestInternal request);
    Task<Result<UpdatedResponse>> UpdateUserRentalListing(Guid rentalListingId, Guid userId, RentalListingUpdateRequestInternal request);
    Task<Result<RentalListingResponse>> GetUserRentalListingById(Guid rentalListingId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result> DeleteUserRentalListing(Guid rentalListingId, Guid userId);
    Task<Result<IReadOnlyCollection<RentalListingListResponse>>> GetAllUserRentalListings(Guid userId, CancellationToken cancellationToken = default);
}
