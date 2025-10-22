using ReviewServices.Core.Domain.RepositoryContracts;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IReviewRepository : IBaseReviewRepository
{
    Task<bool> DeleteReviewAsync(Guid reviewId, CancellationToken cancellationToken = default);
}
