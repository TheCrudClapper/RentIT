using Microsoft.Extensions.Configuration;
using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.Rental;
using ReviewService.Core.DTO.Review;
using ReviewService.Core.Mappings;
using ReviewService.Core.RabbitMQ.Messages;
using ReviewService.Core.ResultTypes;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Core.RabbitMQ.Publishers;
using ReviewServices.Core.ResultTypes;
using ReviewServices.Core.ServiceContracts;

namespace ReviewServices.Core.Services;

public class UserReviewService : IUserReviewService
{
    private readonly IUserReviewRepository _userReviewRepository;
    private readonly IRabbitMQPublisher _rabbitMQPublisher;
    private readonly IConfiguration _configuration;
    private readonly IReviewAllowanceRepository _reviewAllowanceReposiotry;
    private readonly IReviewRepository _reviewRepository;
    private readonly IRentalMicroserviceClient _rentalMicroserviceClient;
    public UserReviewService(IUserReviewRepository userReviewRepository,
        IReviewAllowanceRepository reviewAllowanceRepository,
        IRentalMicroserviceClient rentalMicroserviceClient,
        IRabbitMQPublisher rabbitMQPublisher,
        IReviewRepository reviewRepository,
        IConfiguration configuration)
    {
        _reviewRepository = reviewRepository;
        _userReviewRepository = userReviewRepository;
        _reviewAllowanceReposiotry = reviewAllowanceRepository;
        _rentalMicroserviceClient = rentalMicroserviceClient;
        _rabbitMQPublisher = rabbitMQPublisher;
        _configuration = configuration;
    }

    public async Task<Result<UserReviewResponse>> AddUserReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken)
    {
        var rentalResponse = await _rentalMicroserviceClient.GetRentalByRentalIdAsync(request.RentalId, cancellationToken);
        if (rentalResponse.IsFailure)
            return Result.Failure<UserReviewResponse>(rentalResponse.Error);

        var allowance = await _reviewAllowanceReposiotry
            .GetAllowanceByCondition(item => item.UserId == userId
            && item.RentalId == rentalResponse.Value.Id
            && item.EquipmentId == rentalResponse.Value.EquipmentDetails.Id);

        if (allowance is null || !allowance.IsActive)
            return Result.Failure<UserReviewResponse>(ReviewAllowanceErrors.ReviewAllowanceNotGranted);

        var reviewToAdd = request
            .ToEntity(userId, rentalResponse.Value.EquipmentDetails.Id);

        var reviewFromAdd = await _userReviewRepository.AddUserReviewAsync(reviewToAdd, cancellationToken);

        await _reviewAllowanceReposiotry.DeleteAllowanceAsync(allowance, cancellationToken);

        //publish message
        ReviewCreated reviewCreatedMessage = new(rentalResponse.Value.EquipmentDetails.Id, request.Rating);

        _rabbitMQPublisher.Publish(
            "review.created",
            reviewCreatedMessage,
            _configuration["RABBITMQ_REVIEW_EXCHANGE"]!);

        return reviewFromAdd.ToUserReviewResponse();
    }

    public async Task<Result> DeleteUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _userReviewRepository.GetUserReviewAsync(userId, reviewId, cancellationToken);

        if (review is null)
            return Result.Failure(ReviewErrors.ReviewNotFound);

        await _reviewRepository.DeleteReviewAsync(review);

        //publish deleted message
        ReviewDeleted reviewDeletedMessage = new(review.EquipmentId, review.Rating);

        _rabbitMQPublisher.Publish("review.deleted",
            reviewDeletedMessage,
            _configuration["RABBITMQ_REVIEW_EXCHANGE"]!);

        return Result.Success();
    }

    public async Task<Result<UserReviewResponse>> GetUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _userReviewRepository.GetUserReviewAsync(userId, reviewId, cancellationToken);

        return review is null
            ? Result.Failure<UserReviewResponse>(ReviewErrors.ReviewNotFound)
            : review.ToUserReviewResponse();
    }

    public async Task<Result<UserReviewResponse>> UpdateUserReview(Guid userId, Guid reviewId, ReviewUpdateRequest request, CancellationToken cancellationToken)
    {
        Review entity = new Review
        {
            Description = request.Description,
            Rating = request.Rating,
        };

        var oldRating = await _reviewRepository.GetReviewScoreAsync(reviewId, cancellationToken);
        Review? updatedEntity = await _userReviewRepository.UpdateUserReviewAsync(userId, reviewId, entity, cancellationToken);

        if (updatedEntity is null || oldRating is null )
            return Result.Failure<UserReviewResponse>(ReviewErrors.ReviewNotFound);

        //update message
        ReviewUpdated reviewUpdatedMessage = new(updatedEntity.EquipmentId, updatedEntity.Rating, oldRating.Value);

        _rabbitMQPublisher.Publish("review.updated",
            reviewUpdatedMessage,
            _configuration["RABBITMQ_REVIEW_EXCHANGE"]!);

        return updatedEntity.ToUserReviewResponse();
    }
}
