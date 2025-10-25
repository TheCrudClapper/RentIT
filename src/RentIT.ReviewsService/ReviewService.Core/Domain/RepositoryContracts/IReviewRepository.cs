using ReviewServices.Core.Domain.Entities;
using ReviewServices.Core.Domain.RepositoryContracts;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IReviewRepository : IBaseReviewRepository
{
    Task DeleteReviewAsync(Review review, CancellationToken cancellationToken = default);

    Task<double?> GetReviewScoreAsync(Guid reviewId, CancellationToken cancellationToken = default);
}
