using ReviewService.Core.Domain.ResultTypes;

namespace ReviewService.Core.Domain.Entities.Review.Errors;

public static class ReviewErrors
{
    public static readonly Error NotFound = new Error(
        ErrorType.NotFound, "Review.NotFound", "Review of given id doesn't exist");
}
