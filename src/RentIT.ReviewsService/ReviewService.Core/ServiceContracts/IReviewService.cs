using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.Reviews;

namespace ReviewService.Core.ServiceContracts;

public interface IReviewService
{
    Task<Result<IReadOnlyCollection<ReviewResponse>>> GetReviewsByEquipmentId(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken = default);
    Task<Result> DeleteReview(Guid reviewId, CancellationToken cancellation = default);
}
