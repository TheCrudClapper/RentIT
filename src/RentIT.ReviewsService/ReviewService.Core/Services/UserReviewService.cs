using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.Review;
using ReviewService.Core.Mappings;
using ReviewService.Core.ResultTypes;
using ReviewServices.Core.Domain.Entities;
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

    public Task<Result<ReviewResponse>> AddUserReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        var result = await _userReviewRepository.DeleteUserReviewAsync(userId, reviewId, cancellationToken);

        return result 
            ? Result.Success() 
            : Result.Failure(Error.NotFound);
    }

    public async Task<Result<ReviewResponse>> GetUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _userReviewRepository.GetUserReviewAsync(userId, reviewId, cancellationToken);
        if (review is null)
            return Result.Failure<ReviewResponse>(ReviewErrors.ReviewNotFound);

        var result = await _usersMicroserviceClient.GetUserByUserIdAsync(userId, cancellationToken);

        return result.IsFailure 
            ? Result.Failure<ReviewResponse>(result.Error) 
            : review.ToReviewResponse(result.Value);
    }

    public async Task<Result<UserReviewResponse>> UpdateUserReview(Guid userId, Guid reviewId, ReviewUpdateRequest request, CancellationToken cancellationToken)
    {
        Review entity = request.ToUpdateEntity();
        Review? updatedEntity = await _userReviewRepository.UpdateUserReviewAsync(userId, reviewId, entity, cancellationToken);

        return updatedEntity is null
            ? Result.Failure<UserReviewResponse>(Error.UpdateFailed)
            : updatedEntity.ToUserReviewResponse(); 

    }
}
