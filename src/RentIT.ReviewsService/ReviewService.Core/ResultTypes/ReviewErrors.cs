using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ResultTypes;

public static class ReviewErrors
{
    public static readonly Error ReviewNotFound = new Error(
        404, "Review of given id doesn't exist");

    public static readonly Error ReviewsNotFoundForEquipment = new Error(
        400, "No reviews found for this equipment");
}
