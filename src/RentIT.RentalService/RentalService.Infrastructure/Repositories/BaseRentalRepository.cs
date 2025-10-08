using Microsoft.EntityFrameworkCore;
using RentalService.Core.Domain.Entities;
using RentalService.Core.Domain.RepositoryContracts;
using RentalService.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace RentalService.Infrastructure.Repositories;

public abstract class BaseRentalRepository : IBaseRentalRepository
{
    protected readonly RentalDbContext _context;
    protected BaseRentalRepository(RentalDbContext context)
    {
        _context = context;
    }
    public async Task<Rental?> GetRentalByCondition(Expression<Func<Rental, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        return await _context.Rentals
            .FirstOrDefaultAsync(conditionExpression, cancellationToken);
    }

    public async Task<IEnumerable<Rental>> GetRentalsByCondition(Expression<Func<Rental, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        return await _context.Rentals.Where(conditionExpression)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkEquipmentAsReturned(Rental rental, DateTime returnedDate, CancellationToken cancellationToken)
    {
        rental.ReturnedDate = returnedDate;
        rental.DateEdited = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<Rental?> GetRentalByIdAsync(Guid rentalId, CancellationToken cancellationToken)
    {
        return await _context.Rentals
            .FirstOrDefaultAsync(item => item.Id == rentalId, cancellationToken);
    }

    public async Task UpdateRentalTotalCost(Rental rental, decimal totalCost, CancellationToken cancellationToken)
    {
        rental.RentalPrice = totalCost;
        await _context.SaveChangesAsync(cancellationToken);  
    }
}
