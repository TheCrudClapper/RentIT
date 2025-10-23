using ReviewServices.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ReviewService.Core.Domain.RepositoryContracts;

public interface IReviewAllowanceRepository
{
    Task AddAllowanceAsync(ReviewAllowance allowance, CancellationToken cancellationToken = default);
    Task<bool> DeleteAllowanceAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ReviewAllowance?> GetAllowanceById(Guid id, CancellationToken cancellationToken = default);
    Task<ReviewAllowance?> GetAllowanceByCondition(Expression<Func<ReviewAllowance, bool>> expression, CancellationToken cancellationToken = default);
}
