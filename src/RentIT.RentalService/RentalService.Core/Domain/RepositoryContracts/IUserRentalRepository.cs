using RentalService.Core.Domain.Entities;

namespace RentalService.Core.Domain.RepositoryContracts;

public interface IUserRentalRepository : IBaseRentalRepository
{
    Task<IEnumerable<Rental>> GetAllRentalsAsync(Guid userId, CancellationToken cancellationToken);
    Task<Rental?> GetRentalByIdAsync(Guid rentalId, Guid userId, CancellationToken cancellationToken);
    Task<Rental> AddRentalAsync(Rental rental, Guid userId, CancellationToken cancellationToken);
    Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, Guid userId, CancellationToken cancellationToken);
    Task<bool> DeleteRentalAsync(Guid rentalId, Guid userId, CancellationToken cancellationToken);
}
