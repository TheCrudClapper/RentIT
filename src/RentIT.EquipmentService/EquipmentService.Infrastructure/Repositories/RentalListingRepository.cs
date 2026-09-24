using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public class RentalListingRepository : GenericRepository<RentalListing>, IRentalListingRepository
{
    public RentalListingRepository(EquipmentContext context) : base(context) { }

    public async Task<bool> IsRentalListingUnique(RentalListing entity, Guid? excludeId = null)
    {
        return !await _context.RentalListings
            .AnyAsync(item => item.Title == entity.Title
            && item.EquipmentId == entity.EquipmentId
            && (excludeId == null || item.Id != excludeId));
    }

    public async Task<IReadOnlyCollection<RentalListing>> GetByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken = default)
    {
        return await _context.RentalListings
            .Where(item => item.EquipmentId == equipmentId)
            .ToListAsync(cancellationToken);
    }
}
