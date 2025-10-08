using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;

namespace RentalService.Infrastructure.Repositories;

public class RentalRepository : BaseRentalRepository ,IRentalRepository
{
    public RentalRepository(RentalDbContext context) : base(context) { }

    public async Task<Rental> AddRentalAsync(Rental rental, CancellationToken cancellationToken)
    {
        rental.Id = Guid.NewGuid();
        await _context.Rentals.AddAsync(rental, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return rental;
    }

    public async Task<IEnumerable<Rental>> GetAllRentalsAsync(CancellationToken cancellationToken)
    {
        return await _context.Rentals
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateRentalAsync(Guid rentalId, Rental rental, CancellationToken cancellationToken)
    {
        var rentalToUpdate = await GetRentalByIdAsync(rentalId, cancellationToken);

        if(rentalToUpdate == null)
            return false;

        rentalToUpdate.StartDate = rental.StartDate;
        rentalToUpdate.RentalPrice = rental.RentalPrice;
        rentalToUpdate.ReturnedDate = rental.ReturnedDate;
        rentalToUpdate.EndDate = rental.EndDate;
        rentalToUpdate.UserId = rental.UserId;
        rentalToUpdate.EquipmentId = rental.EquipmentId;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteRentalAsync(Guid rentalId, CancellationToken cancellationToken)
    {
        var rentalToDelete = await GetRentalByIdAsync(rentalId, cancellationToken);

        if(rentalToDelete == null)
            return false;
        rentalToDelete.IsActive = false;
        rentalToDelete.DateDeleted = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteRentalsByEquipmentAsync(Guid equipmentId, CancellationToken cancellationToken)
    {
        //download all rentals where given eq ID
        var rentals = await _context.Rentals
            .Where(rental => rental.EquipmentId == equipmentId)
            .ExecuteUpdateAsync(prop => prop
                .SetProperty(r => r.IsActive, false)
                .SetProperty(item => item.DateDeleted, DateTime.UtcNow),
                cancellationToken);

        return true;
    }
}
