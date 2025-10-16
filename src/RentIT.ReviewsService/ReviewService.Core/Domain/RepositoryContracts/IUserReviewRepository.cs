using ReviewServices.Core.Domain.Entities;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IUserReviewRepository
{
    Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken = default);
    Task<bool> UpdateReviewAsync(Guid id, Review review, CancellationToken cancellationToken = default);
    Task<bool> DeleteReviewAsync(Guid id, CancellationToken cancellationToken = default);
}
