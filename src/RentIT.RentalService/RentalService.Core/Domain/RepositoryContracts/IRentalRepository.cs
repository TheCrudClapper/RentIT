using RentalService.Core.Domain.Entities;
namespace RentalService.Core.Domain.RepositoryContracts
{
    public interface IRentalRepository : IBaseRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental?> GetRentalByIdAsync(Guid rentalId);
        Task<Rental> AddRentalAsync(Rental entity);
        Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental);
        Task<bool> DeleteRentalAsync(Guid rentalId);
    }
}
