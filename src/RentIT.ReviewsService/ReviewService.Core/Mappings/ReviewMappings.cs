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
}
