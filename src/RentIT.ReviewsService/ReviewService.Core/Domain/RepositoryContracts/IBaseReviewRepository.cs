using ReviewService.Core.Domain.Entities.Review;
using System.Linq.Expressions;
namespace ReviewServices.Core.Domain.RepositoryContracts;

public interface IBaseReviewRepository
{
    Task<Review?> GetReviewByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetReviewsByConditionAsync(Expression<Func<Review, bool>> conditionExpression, CancellationToken cancellationToken = default);
    Task<Review?> GetReviewByConditionAsync(Expression<Func<Review, bool>> conditionExpression, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetReviewsByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken = default);
}
