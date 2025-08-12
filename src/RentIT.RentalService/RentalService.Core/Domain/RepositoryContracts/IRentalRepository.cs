using RentalService.Core.Domain.Entities;

namespace RentalService.Core.Domain.RepositoryContracts
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllActiveRentalsAsync();
        Task<Rental?> GetActiveRentalByIdAsync(Guid rentalId);
        Task<Rental> AddRentalAsync(Rental entity);
        Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental);
        Task<bool> DeleteRentalAsync(Guid rentalId);
    }
}
