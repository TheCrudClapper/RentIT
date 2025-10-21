using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.Review;
using ReviewService.Core.Mappings;
using ReviewService.Core.ResultTypes;
using ReviewService.Core.ServiceContracts;
using ReviewServices.Core.ResultTypes;
using System.Collections.Generic;

namespace ReviewServices.Core.Services;

public class ReviewsService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUsersMicroserviceClient _usersMicroserviceClient;
    public ReviewsService(IReviewRepository reviewRepository, IUsersMicroserviceClient usersMicroserviceClient)
    {
        _reviewRepository = reviewRepository;
        _usersMicroserviceClient = usersMicroserviceClient;
    }
    public async Task<Result<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(reviewId, cancellationToken);
        if (review is null)
            return Result.Failure<ReviewResponse>(ReviewErrors.ReviewNotFound);

        var result = await _usersMicroserviceClient.GetUserByUserIdAsync(review.UserId, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<ReviewResponse>(result.Error);

        return review.ToReviewResponse(result.Value);
    }

    public async Task<Result<IEnumerable<ReviewResponse>>> GetReviewsByEquipmentId(Guid equipmentId, CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository.GetReviewsByEquipmentIdAsync(equipmentId, cancellationToken);

        if(!reviews.Any())
            return Result.Failure<IEnumerable<ReviewResponse>>(ReviewErrors.ReviewsNotFoundForEquipment);

        var userIds = reviews
            .Select(item => item.UserId)
            .Distinct()
            .ToList();

        var response = await _usersMicroserviceClient.GetUsersByUsersIdAsync(userIds, cancellationToken);
        if(response.IsFailure)
            return Result.Failure<IEnumerable<ReviewResponse>>(response.Error);

        var userDictionary = response.Value.ToDictionary(u => u.Id);

        var mappedReviews = reviews
            .Where(r => userDictionary.ContainsKey(r.UserId))
            .Select(r => r.ToReviewResponse(userDictionary[r.UserId]))
            .ToList();

        return mappedReviews;
    }
}
