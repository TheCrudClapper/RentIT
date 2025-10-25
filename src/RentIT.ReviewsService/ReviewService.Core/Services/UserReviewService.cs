using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.Rental;
using ReviewService.Core.DTO.Review;
using ReviewService.Core.Mappings;
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
    private readonly IReviewAllowanceRepository _reviewAllowanceReposiotry;
    private readonly IReviewRepository _reviewRepository;
    private readonly IRentalMicroserviceClient _rentalMicroserviceClient;
    public UserReviewService(IUserReviewRepository userReviewRepository,
        IReviewAllowanceRepository reviewAllowanceRepository,
        IRentalMicroserviceClient rentalMicroserviceClient,
        IRabbitMQPublisher rabbitMQPublisher,
        IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
        _userReviewRepository = userReviewRepository;
        _reviewAllowanceReposiotry = reviewAllowanceRepository;
        _rentalMicroserviceClient = rentalMicroserviceClient;
        _rabbitMQPublisher = rabbitMQPublisher;
    }

    public async Task<Result<UserReviewResponse>> AddUserReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken)
    {
        var rentalResponse = await _rentalMicroserviceClient.GetRentalByRentalIdAsync(request.RentalId, cancellationToken);
        if (rentalResponse.IsFailure)
            return Result.Failure<UserReviewResponse>(rentalResponse.Error);

        var allowance = await _reviewAllowanceReposiotry
            .GetAllowanceByCondition(item => item.UserId == userId
            && item.RentalId == rentalResponse.Value.Id
            && item.EquipmentId == rentalResponse.Value.equipmentDetails.Id);

        if (allowance is null)
            return Result.Failure<UserReviewResponse>(ReviewAllowanceErrors.ReviewAllowanceNotGranted);

        var reviewToAdd = request
            .ToEntity(userId, rentalResponse.Value.equipmentDetails.Id);

        var reviewFromAdd = await _userReviewRepository.AddUserReviewAsync(reviewToAdd, cancellationToken);

        await _reviewAllowanceReposiotry.DeleteAllowanceAsync(allowance);

        //project message review.created messae 

        return reviewFromAdd.ToUserReviewResponse();
    }

    public async Task<Result> DeleteUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await _userReviewRepository.GetUserReviewAsync(userId, reviewId, cancellationToken);

        if (review is null)
            return Result.Failure(Error.DeleteFailed);

        await _reviewRepository.DeleteReviewAsync(review);

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
        Review entity = request.ToEntity();
        Review? updatedEntity = await _userReviewRepository.UpdateUserReviewAsync(userId, reviewId, entity, cancellationToken);

        return updatedEntity is null
            ? Result.Failure<UserReviewResponse>(Error.UpdateFailed)
            : updatedEntity.ToUserReviewResponse(); 

    }
}
