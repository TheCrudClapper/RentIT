using RentalService.Core.Domain.Entities;
using System.Linq.Expressions;

namespace RentalService.Core.Domain.RepositoryContracts
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetRentalsByCondition(Expression<Func<Rental, bool>> conditionExpression);
        Task<Rental?> GetRentalByCondition(Expression<Func<Rental, bool>> conditionExpression);
        Task<IEnumerable<Rental>> GetAllUserRentalsAsync(Guid userId);
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<Rental?> GetRentalByIdAsync(Guid rentalId);
        Task<Rental?> GetUserRentalByIdAsync(Guid rentalId , Guid userId);
        Task<Rental> AddRentalAsync(Rental entity);
        Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental);
        Task<bool> DeleteRentalAsync(Guid rentalId);
    }
}
