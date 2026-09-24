using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.RentalListings;

namespace EquipmentService.Core.ServiceContracts;

public interface IRentalListingService
{
    Task<Result<RentalListingResponse>> GetRentalListing(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<RentalListingListResponse>>> GetAllRentalListings(CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<RentalListingResponse>>> GetRentalListingsByEquipmentId(Guid equipmentId, CancellationToken cancellationToken = default);
}
