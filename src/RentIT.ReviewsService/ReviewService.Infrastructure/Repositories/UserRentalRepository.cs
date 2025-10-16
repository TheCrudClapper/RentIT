using ReviewService.Core.Domain.RepositoryContracts;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Infrastructure.DbContexts;
using ReviewServices.Infrastructure.Repositories;

namespace ReviewService.Infrastructure.Repositories;

public class UserRentalRepository : BaseReviewRepository, IUserReviewRepository
{
    public UserRentalRepository(ReviewsDbContext context) : base(context) {}

    public async Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken = default)
    {
        review.Id = Guid.NewGuid();
        await _context.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return review;
    }

    public async Task<bool> DeleteReviewAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var review = await GetReviewByIdAsync(id, cancellationToken);
        if (review is null)
            return false;

        _context.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> UpdateReviewAsync(Guid id, Review review, CancellationToken cancellationToken = default)
    {
        var reviewToUpdate = await GetReviewByIdAsync(id, cancellationToken);
        if (reviewToUpdate is null)
            return false;

        reviewToUpdate.Description = review.Description;
        reviewToUpdate.DateEdited = DateTime.UtcNow;
        reviewToUpdate.Rating = review.Rating;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
