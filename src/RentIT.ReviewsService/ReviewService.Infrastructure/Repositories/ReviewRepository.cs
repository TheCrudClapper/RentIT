using ReviewService.Core.Domain.RepositoryContracts;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Infrastructure.DbContexts;
using ReviewServices.Infrastructure.Repositories;

namespace ReviewService.Infrastructure.Repositories;

public class ReviewRepository : BaseReviewRepository, IReviewRepository
{
    public ReviewRepository(ReviewsDbContext context) : base(context)
    {
    }
    public async Task DeleteReviewAsync(Review review, CancellationToken cancellationToken)
    {
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
