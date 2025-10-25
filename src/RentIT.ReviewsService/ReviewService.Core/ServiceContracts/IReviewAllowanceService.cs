using ReviewService.Core.DTO.ReviewAllowance;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ServiceContracts;

public interface IReviewAllowanceService
{
    Task<Result<IEnumerable<ReviewAllowanceResponse>>> GetAllReviewAllowances(CancellationToken cancellationToken = default);
    Task AddReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAllowance(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken = default);
    Task<Result<bool>> DoesAllowanceExist(Guid userId, Guid rentalId, Guid equipmentId);
}
