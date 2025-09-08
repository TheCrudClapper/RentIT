using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace RentalService.Infrastructure.Repositories
{
    public class UserRentalRepository : BaseRentalRepository,IUserRentalRepository 
    {
        public UserRentalRepository(RentalDbContext context) : base(context) { }
        public Task<Rental> AddRentalAsync(Rental rental, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRentalAsync(Guid rentalId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync(Guid userId)
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<Rental?> GetRentalByCondition(Expression<Func<Rental, bool>> conditionExpression)
        {
            return await _context.Rentals
                .FirstOrDefaultAsync(conditionExpression);
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCondition(Expression<Func<Rental, bool>> conditionExpression)
        {
            return await _context.Rentals.Where(conditionExpression)
                .ToListAsync();
        }

        public async Task<Rental?> GetRentalByIdAsync(Guid rentalId, Guid userId)
        {
            return await _context.Rentals
                .FirstOrDefaultAsync(item => item.Id == rentalId &&  item.UserId == userId);
        }

       

        public Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
