using ReviewService.Core.Domain.Entities.Review;
using ReviewServices.Core.Domain.RepositoryContracts;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IUserReviewRepository : IBaseReviewRepository
{
    Task<Review> AddUserReviewAsync(Review review, CancellationToken cancellationToken = default);
    Task<Review?> UpdateUserReviewAsync(Guid userId, Guid reviewId, Review review, CancellationToken cancellationToken = default);
    Task<Review?> GetUserReviewAsync(Guid userId, Guid reviewId, CancellationToken cancellationToken = default);
}
