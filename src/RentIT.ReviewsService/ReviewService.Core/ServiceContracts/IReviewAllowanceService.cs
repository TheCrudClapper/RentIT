using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.ReviewAllowance;

namespace ReviewService.Core.ServiceContracts;

public interface IReviewAllowanceService
{
    Task<Result<IEnumerable<ReviewAllowanceResponse>>> GetAllReviewAllowances(CancellationToken cancellationToken = default);
    Task AddReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAllowance(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken = default);
}
