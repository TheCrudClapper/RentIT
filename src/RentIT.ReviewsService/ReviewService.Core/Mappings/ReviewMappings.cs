using ReviewService.Core.DTO;
using ReviewServices.Core.Domain.Entities;
using ReviewServices.Core.DTO;

namespace ReviewService.Core.Mappings;

public static  class ReviewMappings
{
    public static ReviewResponse ToReviewResponse(this Review review, UserResponse userResponse)
    {
        return new ReviewResponse(review.Id, userResponse.Email, review.Description, review.Rating);
    }
}
