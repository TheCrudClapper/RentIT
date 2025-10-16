using ReviewServices.Core.DTO;
using ReviewServices.Core.ResultTypes;

namespace ReviewServices.Core.ServiceContracts;

/// <summary>
/// Defines operations for adding, updating, retrieving, and deleting user reviews.
/// </summary>
/// <remarks>Implementations of this interface should ensure appropriate validation and authorization for review
/// operations. Methods are asynchronous and support cancellation via the provided token.</remarks>
public interface IUserReviewService
{
    Task<Result<ReviewResponse>> AddReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken = default);
    Task<Result<ReviewResponse>> UpdateReview(Guid userId, ReviewUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result<ReviewResponse>> GetReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default);
    Task<Result> DeleteReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default);
}
