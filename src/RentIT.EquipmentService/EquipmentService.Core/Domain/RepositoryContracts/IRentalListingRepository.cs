using EquipmentService.Core.Domain.Entities.Listing;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IRentalListingRepository : IGenericRepository<RentalListing>
{
    Task<bool> IsRentalListingUnique(RentalListing dbObject, Guid? excludeId = null);
    Task<IReadOnlyCollection<RentalListing>> GetByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken = default);
}
