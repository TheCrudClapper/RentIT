using ReviewService.Core.DTO.ReviewAllowance;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ServiceContracts;

public interface IReviewAllowanceService
{
    Task<Result<ReviewAllowanceResponse>> AddReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> RevokeAllowance(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken = default);
}
