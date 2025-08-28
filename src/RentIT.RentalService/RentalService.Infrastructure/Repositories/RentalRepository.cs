using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalDbContext _context;
        public RentalRepository(RentalDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> AddRentalAsync(Rental rental)
        {
            rental.Id = Guid.NewGuid();
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Rental?> GetRentalByIdAsync(Guid rentalId)
        {
            return await _context.Rentals
                .FirstOrDefaultAsync(item => item.Id == rentalId);
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _context.Rentals
                .ToListAsync();
        }

        //Main purpose method, later will include user who wanted to edit this 
        //and checking 
        public async Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental)
        {
            var rentalToUpdate = await GetRentalByIdAsync(rentalId);

            if(rentalToUpdate == null)
                return false;

            rentalToUpdate.StartDate = rental.StartDate;
            rentalToUpdate.RentalPrice = rental.RentalPrice;
            rentalToUpdate.ReturnedDate = rental.ReturnedDate;
            rentalToUpdate.EndDate = rental.EndDate;
            rentalToUpdate.UserId = rental.UserId;
            rentalToUpdate.EquipmentId = rental.EquipmentId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRentalAsync(Guid rentalId)
        {
            var rentalToDelete = await GetRentalByIdAsync(rentalId);

            if(rentalToDelete == null)
                return false;

            rentalToDelete.IsActive = false;
            rentalToDelete.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Rental>> GetAllUserRentalsAsync(Guid userId)
        {
            return await _context.Rentals
                .Where(item => item.UserId == userId)
                .ToListAsync();
        }

        public async Task<Rental?> GetUserRentalByIdAsync(Guid rentalId, Guid userId)
        {
            return await _context.Rentals
                .FirstOrDefaultAsync(item => item.Id == rentalId && item.UserId == userId);
        }
    }
}
