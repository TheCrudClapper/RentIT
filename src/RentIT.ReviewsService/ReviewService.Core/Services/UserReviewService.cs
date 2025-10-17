using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.ResultTypes;
using ReviewServices.Core.DTO;
using ReviewServices.Core.ResultTypes;
using ReviewServices.Core.ServiceContracts;

namespace ReviewServices.Core.Services;

public class UserReviewService : IUserReviewService
{
    private readonly IUserReviewRepository _userReviewRepository;
    public UserReviewService(IUserReviewRepository userReviewRepository)
    {
        _userReviewRepository = userReviewRepository;
    }

    public Task<Result<ReviewResponse>> AddReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ReviewResponse>> GetReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        var review = await _userReviewRepository.GetUserReviewAsync(reviewId, userId, cancellationToken);
        if (review is null)
            return Result.Failure<ReviewResponse>(ReviewErrors.ReviewNotFound);

        //fetch user from microservice, populate field in response and return 

    }

    public Task<Result<ReviewResponse>> UpdateReview(Guid userId, ReviewUpdateRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
