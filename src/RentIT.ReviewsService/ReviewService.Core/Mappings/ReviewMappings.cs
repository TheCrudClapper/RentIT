using ReviewService.Core.DTO.Review;
using ReviewService.Core.DTO.User;
using ReviewServices.Core.Domain.Entities;

namespace ReviewService.Core.Mappings;

public static  class ReviewMappings
{
    public static ReviewResponse ToReviewResponse(this Review review, UserResponse userResponse)
    {
        return new ReviewResponse(review.Id, userResponse.Email, review.Description, review.Rating);
    }
    public static UserReviewResponse ToUserReviewResponse(this Review review)
    {
        return new UserReviewResponse(review.Id, review.Description, review.Rating);
    }

    public static Review ToUpdateEntity(this ReviewUpdateRequest dto)
    {
        return new Review
        {
            Rating = dto.Rating,
            Description = dto.Description,
        };
    }
}
