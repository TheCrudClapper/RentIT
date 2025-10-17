using ReviewServices.Core.Domain.Entities;
using ReviewServices.Core.Domain.RepositoryContracts;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IUserReviewRepository : IBaseReviewRepository
{
    Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken = default);
    Task<bool> UpdateReviewAsync(Guid id, Review review, CancellationToken cancellationToken = default);
    Task<bool> DeleteReviewAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Review?> GetUserReviewAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}
