using ReviewService.Core.DTO.ReviewAllowance;
using ReviewServices.Core.Domain.Entities;

namespace ReviewService.Core.Mappings;

public static class ReviewAllowanceMappings
{
    public static ReviewAllowanceResponse ToReviewAllowanceResponse(this ReviewAllowance allowance)
    {
        return new ReviewAllowanceResponse(
            allowance.UserId,
            allowance.EquipmentId, 
            allowance.RentalId);
    }
}
