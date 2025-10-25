using Microsoft.EntityFrameworkCore;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace ReviewService.Infrastructure.Repositories;

public class ReviewAllowanceRepository : IReviewAllowanceRepository
{
    private readonly ReviewsDbContext _context;
    public ReviewAllowanceRepository(ReviewsDbContext context)
    {
        _context = context;
    }
    public async Task AddAllowanceAsync(ReviewAllowance allowance, CancellationToken cancellationToken)
    {
        allowance.Id = Guid.NewGuid();
        allowance.DateCreated = DateTime.UtcNow;
        allowance.IsActive = true;
        await _context.ReviewsAllowance.AddAsync(allowance);
        await _context.SaveChangesAsync();
    }


    public async Task<ReviewAllowance?> GetAllowanceByCondition(Expression<Func<ReviewAllowance, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.ReviewsAllowance
            .Where(expression)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ReviewAllowance?> GetAllowanceById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ReviewsAllowance
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public async Task DeleteAllowanceAsync(ReviewAllowance allowance, CancellationToken cancellationToken)
    {
        _context.ReviewsAllowance.Remove(allowance);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ReviewAllowance>> GetAllReviewAllowances(CancellationToken cancellationToken)
    {
        return await _context.ReviewsAllowance
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsAllowanceUnique(ReviewAllowance allowance)
    {
        return !await _context.ReviewsAllowance
            .AnyAsync(item => item.UserId == allowance.UserId
            && item.RentalId == allowance.RentalId
            && item.EquipmentId == allowance.EquipmentId);
    }
}
