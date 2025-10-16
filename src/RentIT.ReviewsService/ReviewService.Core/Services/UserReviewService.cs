using ReviewServices.Core.DTO;
using ReviewServices.Core.ResultTypes;
using ReviewServices.Core.ServiceContracts;

namespace ReviewServices.Core.Services;

public class UserReviewService : IUserReviewService
{
    private readonly IUserReviewService _userReviewService;
    public UserReviewService(IUserReviewService userReviewRepository)
    {
        _userReviewService = userReviewRepository;
    }

    public Task<Result<ReviewResponse>> AddReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ReviewResponse>> GetReview(Guid userId, Guid reviewId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ReviewResponse>> UpdateReview(Guid userId, ReviewUpdateRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
