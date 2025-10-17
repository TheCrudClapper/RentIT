using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ResultTypes;

public static class ReviewErrors
{
    public static readonly Error ReviewNotFound = new Error(
        404, "Review of given id doesn't exist");
}
