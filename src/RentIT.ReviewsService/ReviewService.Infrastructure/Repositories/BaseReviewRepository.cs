using Microsoft.EntityFrameworkCore;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Core.Domain.RepositoryContracts;
using ReviewServices.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace ReviewServices.Infrastructure.Repositories;

public class BaseReviewRepository : IBaseReviewRepository
{
    protected readonly ReviewsDbContext _context;
    public BaseReviewRepository(ReviewsDbContext context)
    {
        _context = context;
    }
    public async Task<Review?> GetReviewByConditionAsync(Expression<Func<Review, bool>> conditionExpression, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews.Where(conditionExpression)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetReviewsByConditionAsync(Expression<Func<Review, bool>> conditionExpression, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(conditionExpression).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Review?>> GetReviewsByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews.Where(item => item.EquipmentId == equipmentId)
            .ToListAsync(cancellationToken);
    }
}
