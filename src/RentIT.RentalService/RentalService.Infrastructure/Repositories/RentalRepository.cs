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
            await _context.SaveChangesAsync();

            //loading up navigation properties to return to user full response object
            await _context.Entry(rental).Reference(item => item.CreatedBy).LoadAsync();
            await _context.Entry(rental).Reference(item => item.Equipment).LoadAsync();
            return rental;
        }

        public async Task<Rental?> GetActiveRentalByIdAsync(Guid rentalId)
        {
            return await _context.Rentals
                .Include(item => item.CreatedBy)
                .Include(item => item.Equipment)
                .FirstOrDefaultAsync(item => item.IsActive && item.Id == rentalId);
        }

        public async Task<IEnumerable<Rental>> GetAllActiveRentalsAsync()
        {
            return await _context.Rentals
                .Where(item => item.IsActive)
                .Include(item => item.Equipment)
                .Include(item => item.CreatedBy)
                .ToListAsync();
        }

        public async Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental)
        {
            var rentalToUpdate = await GetActiveRentalByIdAsync(rentalId);

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
            var rentalToDelete = await GetActiveRentalByIdAsync(rentalId);

            if(rentalToDelete == null)
                return false;

            rentalToDelete.IsActive = false;
            rentalToDelete.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
