using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.Review;
using ReviewService.Core.Mappings;
using ReviewService.Core.ResultTypes;
using ReviewServices.Core.ResultTypes;
using ReviewServices.Core.ServiceContracts;

namespace ReviewServices.Core.Services;

public class UserReviewService : IUserReviewService
{
    private readonly IUserReviewRepository _userReviewRepository;
    private readonly IUsersMicroserviceClient _usersMicroserviceClient;
    public UserReviewService(IUserReviewRepository userReviewRepository, IUsersMicroserviceClient usersMicroserviceClient)
    {
        _userReviewRepository = userReviewRepository;
        _usersMicroserviceClient = usersMicroserviceClient;
    }

    public Task<Result<ReviewResponse>> AddReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ReviewResponse>> GetReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _userReviewRepository.GetUserReviewAsync(reviewId, userId, cancellationToken);
        if (review is null)
            return Result.Failure<ReviewResponse>(ReviewErrors.ReviewNotFound);

        var result = await _usersMicroserviceClient.GetUserByUserIdAsync(userId, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<ReviewResponse>(result.Error);

        return review.ToReviewResponse(result.Value);
    }

    public Task<Result<ReviewResponse>> UpdateReview(Guid userId, ReviewUpdateRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
