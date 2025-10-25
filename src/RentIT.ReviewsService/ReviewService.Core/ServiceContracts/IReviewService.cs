using ReviewService.Core.DTO.Review;
using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ServiceContracts;

public interface IReviewService
{
    Task<Result<IEnumerable<ReviewResponse>>> GetReviewsByEquipmentId(Guid equipmentId, CancellationToken cancellationToken = default);
    Task<Result<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken = default);
    Task<Result> DeleteReview(Guid reviewId, CancellationToken cancellation = default);
}
