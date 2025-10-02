using RentalService.Core.Domain.Entities;
namespace RentalService.Core.Domain.RepositoryContracts;

public interface IRentalRepository : IBaseRentalRepository
{
    Task<IEnumerable<Rental>> GetAllRentalsAsync(CancellationToken cancellationToken);
    Task<Rental?> GetRentalByIdAsync(Guid rentalId, CancellationToken cancellationToken);
    Task<Rental> AddRentalAsync(Rental entity, CancellationToken cancellationToken);
    Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, CancellationToken cancellationToken);
    Task<bool> DeleteRentalAsync(Guid rentalId, CancellationToken cancellationToken);
    Task<bool> DeleteRentalsByEquipmentAsync(Guid equipmentId, CancellationToken cancellationToken);
}
