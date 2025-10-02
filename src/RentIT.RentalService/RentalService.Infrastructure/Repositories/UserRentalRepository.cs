using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Repositories;

public class UserRentalRepository : BaseRentalRepository, IUserRentalRepository 
{
    public UserRentalRepository(RentalDbContext context) : base(context) { }
    public async Task<Rental> AddRentalAsync(Rental rental, Guid userId, CancellationToken cancellationToken)
    {
        rental.Id = Guid.NewGuid();
        await _context.Rentals.AddAsync(rental, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return rental;
    }

    public async Task<bool> DeleteRentalAsync(Guid rentalId, Guid userId, CancellationToken cancellationToken)
    {
        var rentalToDelete = await GetRentalByIdAsync(rentalId, userId, cancellationToken);

        if (rentalToDelete == null)
            return false;

        rentalToDelete.IsActive = false;
        rentalToDelete.DateDeleted = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Rentals
           .AsNoTracking()
           .Where(item => item.UserId == userId)
           .ToListAsync(cancellationToken);
    }
    public async Task<Rental?> GetRentalByIdAsync(Guid rentalId, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Rentals
            .FirstOrDefaultAsync(item => item.Id == rentalId &&  item.UserId == userId, cancellationToken);
    }

    //User can only change start and end date
    public async Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, Guid userId, CancellationToken cancellationToken)
    {
        var rentalToUpdate = await GetRentalByIdAsync(rentalId, userId, cancellationToken);

        if (rentalToUpdate == null)
            return false;

        rentalToUpdate.StartDate = rental.StartDate;
        rentalToUpdate.EndDate = rental.EndDate;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
