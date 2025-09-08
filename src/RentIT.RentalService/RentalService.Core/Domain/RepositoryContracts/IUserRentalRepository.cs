using RentalService.Core.Domain.Entities;
using System.Linq.Expressions;

namespace RentalService.Core.Domain.RepositoryContracts
{
    public interface IUserRentalRepository : IBaseRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllRentalsAsync(Guid userId);
        Task<Rental?> GetRentalByIdAsync(Guid rentalId, Guid userId);
        Task<Rental> AddRentalAsync(Rental rental, Guid userId);
        Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, Guid userId);
        Task<bool> DeleteRentalAsync(Guid rentalId, Guid userId);
    }
}
