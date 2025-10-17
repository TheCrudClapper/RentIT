using ReviewServices.Core.Domain.Entities;
using ReviewServices.Core.DTO;

namespace ReviewService.Core.Mappings;

public static  class ReviewMappings
{
    public static ReviewResponse ToReviewResponse(this Review review)
    {

        return new ReviewResponse( review.Description, review.Rating);
    }
}
