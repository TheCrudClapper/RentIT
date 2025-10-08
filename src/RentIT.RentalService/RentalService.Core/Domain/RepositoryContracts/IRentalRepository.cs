using RentalService.Core.Domain.Entities;
namespace RentalService.Core.Domain.RepositoryContracts;

public interface IRentalRepository : IBaseRentalRepository
{
    Task<IEnumerable<Rental>> GetAllRentalsAsync(CancellationToken cancellationToken = default);
    Task<Rental> AddRentalAsync(Rental entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, CancellationToken cancellationToken = default);
    Task<bool> DeleteRentalAsync(Guid rentalId, CancellationToken cancellationToken = default);
    Task<bool> DeleteRentalsByEquipmentAsync(Guid equipmentId, CancellationToken cancellationToken = default);
}
