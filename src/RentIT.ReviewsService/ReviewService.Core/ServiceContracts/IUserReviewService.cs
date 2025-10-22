using ReviewService.Core.DTO.Review;
using ReviewServices.Core.ResultTypes;

namespace ReviewServices.Core.ServiceContracts;

/// <summary>
/// Defines operations for adding, updating, retrieving, and deleting user reviews.
/// </summary>
/// <remarks>Implementations of this interface should ensure appropriate validation and authorization for review
/// operations. Methods are asynchronous and support cancellation via the provided token.</remarks>
public interface IUserReviewService
{
    Task<Result<ReviewResponse>> AddUserReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken = default);
    Task<Result<UserReviewResponse>> UpdateUserReview(Guid userId, Guid reviewId, ReviewUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result<ReviewResponse>> GetUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default);
    Task<Result> DeleteUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default);
}
