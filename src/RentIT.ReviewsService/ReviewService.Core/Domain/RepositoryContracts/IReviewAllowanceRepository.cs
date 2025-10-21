using ReviewServices.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IReviewAllowanceRepository
{
    Task<ReviewAllowance> AddAllowanceAsync(ReviewAllowance allowance, CancellationToken cancellationToken = default);
    Task<bool> RevokeAllowanceAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ReviewAllowance?> GetAllowanceById(Guid id, CancellationToken cancellationToken = default);
    Task<ReviewAllowance?> GetAllowanceByCondition(Expression<Func<ReviewAllowance, bool>> expression, CancellationToken cancellationToken = default);
}
