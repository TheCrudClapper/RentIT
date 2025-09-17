using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Repositories
{
    public class UserRentalRepository : BaseRentalRepository,IUserRentalRepository 
    {
        public UserRentalRepository(RentalDbContext context) : base(context) { }
        public async Task<Rental> AddRentalAsync(Rental rental, Guid userId)
        {
            rental.Id = Guid.NewGuid();
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<bool> DeleteRentalAsync(Guid rentalId, Guid userId)
        {
            var rentalToDelete = await GetRentalByIdAsync(rentalId, userId);

            if (rentalToDelete == null)
                return false;

            rentalToDelete.IsActive = false;
            rentalToDelete.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync(Guid userId)
        {
            return await _context.Rentals
               .AsNoTracking()
               .Where(item => item.UserId == userId)
               .ToListAsync();
        }
        public async Task<Rental?> GetRentalByIdAsync(Guid rentalId, Guid userId)
        {
            return await _context.Rentals
                .FirstOrDefaultAsync(item => item.Id == rentalId &&  item.UserId == userId);
        }

        //User can only change start and end date
        public async Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, Guid userId)
        {
            var rentalToUpdate = await GetRentalByIdAsync(rentalId, userId);

            if (rentalToUpdate == null)
                return false;

            rentalToUpdate.StartDate = rental.StartDate;
            rentalToUpdate.EndDate = rental.EndDate;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
