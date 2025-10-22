using ReviewService.Core.Domain.RepositoryContracts;
using ReviewServices.Infrastructure.DbContexts;
using ReviewServices.Infrastructure.Repositories;

namespace ReviewService.Infrastructure.Repositories;

public class ReviewRepository : BaseReviewRepository, IReviewRepository
{
    public ReviewRepository(ReviewsDbContext context) : base(context)
    {
    }
    public async Task<bool> DeleteReviewAsync(Guid reviewId, CancellationToken cancellationToken = default)
    {
        var review = await GetReviewByIdAsync(reviewId, cancellationToken);
        if (review is null)
            return false;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return true;
    }
}
