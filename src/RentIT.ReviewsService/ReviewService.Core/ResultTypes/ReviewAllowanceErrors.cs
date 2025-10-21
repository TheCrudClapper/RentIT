using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ResultTypes;

public class ReviewAllowanceErrors
{
    public static readonly Error ReviewAllowanceNotFound = new Error
        (404, "Review allowance of this id not found");
}
