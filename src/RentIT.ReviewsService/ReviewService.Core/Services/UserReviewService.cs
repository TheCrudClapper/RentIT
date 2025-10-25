using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.RepositoryContracts;
using ReviewService.Core.DTO.Rental;
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
    private readonly IReviewAllowanceRepository _reviewAllowanceReposiotry;
    private readonly IRentalMicroserviceClient _rentalMicroserviceClient;
    public UserReviewService(IUserReviewRepository userReviewRepository,
        IUsersMicroserviceClient usersMicroserviceClient,
        IReviewAllowanceRepository reviewAllowanceRepository,
        IRentalMicroserviceClient rentalMicroserviceClient)
    {
        _userReviewRepository = userReviewRepository;
        _usersMicroserviceClient = usersMicroserviceClient;
        _reviewAllowanceReposiotry = reviewAllowanceRepository;
        _rentalMicroserviceClient = rentalMicroserviceClient;
    }

    public async Task<Result<UserReviewResponse>> AddUserReview(Guid userId, ReviewAddRequest request, CancellationToken cancellationToken)
    {
        var rentalResponse = await _rentalMicroserviceClient.GetRentalByRentalIdAsync(request.RentalId, cancellationToken);
        if (rentalResponse.IsFailure)
            return Result.Failure<UserReviewResponse>(rentalResponse.Error);

        //validate allowance
        bool allowanceExist = await _reviewAllowanceReposiotry
            .DoesAllowanceExists(userId, rentalResponse.Value.Id, rentalResponse.Value.equipmentDetails.Id);

        if (!allowanceExist)
            return Result.Failure<UserReviewResponse>(ReviewAllowanceErrors.ReviewAllowanceNotGranted);

        var reviewToAdd = request
            .ToEntity(userId, rentalResponse.Value.equipmentDetails.Id);

        var reviewFromAdd = await _userReviewRepository.AddUserReviewAsync(reviewToAdd, cancellationToken);

        //produce a message for updation of total score of equipment
        return reviewFromAdd.ToUserReviewResponse();
    }

    public async Task<Result> DeleteUserReview(Guid userId, Guid reviewId, CancellationToken cancellationToken)
    {
        var result = await _userReviewRepository.DeleteUserReviewAsync(userId, reviewId, cancellationToken);

        return result 
            ? Result.Success() 
            : Result.Failure(Error.NotFound);
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
