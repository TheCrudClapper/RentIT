using ReviewService.Core.Domain.Entities.Review.Errors;
using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.Reviews;
using ReviewService.Core.Mappings;
using ReviewService.Core.ServiceContracts;
namespace ReviewServices.Core.Services;

public class ReviewsService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUsersMicroserviceClient _usersMicroserviceClient;
    public ReviewsService(IReviewRepository reviewRepository,
        IUsersMicroserviceClient usersMicroserviceClient)
    {
        _reviewRepository = reviewRepository;
        _usersMicroserviceClient = usersMicroserviceClient;
    }

    public async Task<Result> DeleteReview(Guid reviewId, CancellationToken cancellation = default)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(reviewId, cancellation);

        if (review is null)
            return Result.Failure(ReviewErrors.NotFound);

        await _reviewRepository.DeleteReviewAsync(review);

        return Result.Success();
    }

    public async Task<Result<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(reviewId, cancellationToken);
        if (review is null)
            return Result.Failure<ReviewResponse>(ReviewErrors.NotFound);

        var result = await _usersMicroserviceClient.GetUserByUserIdAsync(review.UserId, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<ReviewResponse>(result.Error);

        return review.ToReviewResponse(result.Value);
    }

    public async Task<Result<IReadOnlyCollection<ReviewResponse>>> GetReviewsByEquipmentId(Guid equipmentId, CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository.GetReviewsByEquipmentIdAsync(equipmentId, cancellationToken);

        if (!reviews.Any())
            return new List<ReviewResponse>();

        var userIds = reviews
            .Select(item => item.UserId)
            .Distinct()
            .ToList();

        var response = await _usersMicroserviceClient.GetUsersByUsersIdsAsync(userIds, cancellationToken);
        if (response.IsFailure)
            return Result.Failure<IReadOnlyCollection<ReviewResponse>>(response.Error);

        var userDictionary = response.Value.ToDictionary(u => u.Id);

        var mappedReviews = reviews
            .Where(r => userDictionary.ContainsKey(r.UserId))
            .Select(r => r.ToReviewResponse(userDictionary[r.UserId]))
            .ToList();

        return mappedReviews;
    }
}
