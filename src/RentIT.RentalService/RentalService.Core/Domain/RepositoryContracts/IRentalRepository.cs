using RentalService.Core.Domain.Entities;

namespace RentalService.Core.Domain.RepositoryContracts
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllActiveUserRentalsAsync(Guid userId);
        Task<IEnumerable<Rental>> GetAllActiveRentalsAsync();
        Task<Rental?> GetActiveRentalByIdAsync(Guid rentalId);
        Task<Rental?> GetRentalByIdAsync (Guid rentalId);
        Task<Rental?> GetActiveUserRentalByIdAsync(Guid rentalId , Guid userId);
        Task<Rental> AddRentalAsync(Rental entity);
        Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental);
        Task<bool> DeleteRentalAsync(Guid rentalId);
    }
}
