using Microsoft.EntityFrameworkCore;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Infrastructure.DbContexts;
using ReviewServices.Infrastructure.Repositories;

namespace ReviewService.Infrastructure.Repositories;

public class UserReviewRepository : BaseReviewRepository, IUserReviewRepository
{
    public UserReviewRepository(ReviewsDbContext context) : base(context) {}

    public async Task<Review> AddUserReviewAsync(Review review, CancellationToken cancellationToken)
    {
        review.Id = Guid.NewGuid();
        review.DateCreated = DateTime.UtcNow;
        review.IsActive = true;
        await _context.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return review;
    }

    public async Task<bool> DeleteUserReviewAsync(Guid userId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        var review = await GetUserReviewAsync(userId, reviewId, cancellationToken);
        if (review is null)
            return false;

        _context.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Review?> GetUserReviewAsync(Guid reviewId, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(item => item.Id == reviewId && item.UserId == userId, cancellationToken);
    }

    public async Task<bool> UpdateUserReviewAsync(Guid reviewId, Review review, CancellationToken cancellationToken)
    {
        var reviewToUpdate = await GetReviewByIdAsync(reviewId, cancellationToken);
        if (reviewToUpdate is null)
            return false;

        reviewToUpdate.Description = review.Description;
        reviewToUpdate.DateEdited = DateTime.UtcNow;
        reviewToUpdate.Rating = review.Rating;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Review?> UpdateUserReviewAsync(Guid userId, Guid reviewId, Review review, CancellationToken cancellationToken = default)
    {
        var reviewToUpdate = await GetUserReviewAsync(reviewId, userId, cancellationToken);
        if (reviewToUpdate is null)
            return null;

        reviewToUpdate.Description = review.Description;
        reviewToUpdate.DateEdited = DateTime.UtcNow;
        reviewToUpdate.Rating = review.Rating;

        await _context.SaveChangesAsync(cancellationToken);
        return reviewToUpdate;
    }
}
