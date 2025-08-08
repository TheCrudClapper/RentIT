using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Infrastructure.DbContexts;

namespace RentIT.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;
        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> AddRentalAsync(Rental rental)
        {
            rental.Id = Guid.NewGuid();
            _context.Rentals.Add(rental);

            //loading up navigation properties to return to user full response object
            await _context.Entry(rental).Reference(item => item.RentedBy).LoadAsync();
            await _context.Entry(rental).Reference(item => item.Equipment).LoadAsync();

            rental.Equipment.Status = RentStatusEnum.Rented;
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<Rental?> GetActiveRentalByIdAsync(Guid rentalId)
        {
            return await _context.Rentals
                .Include(item => item.RentedBy)
                .Include(item => item.Equipment)
                .FirstOrDefaultAsync(item => item.IsActive && item.Id == rentalId);
        }

        public async Task<IEnumerable<Rental>> GetAllActiveRentalsAsync()
        {
            return await _context.Rentals
                .Where(item => item.IsActive)
                .Include(item => item.Equipment)
                .Include(item => item.RentedBy)
                .ToListAsync();
        }

        public async Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental)
        {
            var rentalToUpdate = await GetActiveRentalByIdAsync(rentalId);

            if(rentalToUpdate == null)
                return false;

            rentalToUpdate.StartDate = rental.StartDate;
            rentalToUpdate.ReturnedDate = rental.ReturnedDate;
            rentalToUpdate.EndDate = rental.EndDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRentalAsync(Guid rentalId)
        {
            var rentalToDelete = await GetActiveRentalByIdAsync(rentalId);

            if(rentalToDelete == null)
                return false;

            rentalToDelete.Equipment.Status = RentStatusEnum.Avaliable;
            rentalToDelete.IsActive = false;
            rentalToDelete.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
