using ReviewServices.Core.ResultTypes;

namespace ReviewService.Core.ResultTypes;

public class ReviewAllowanceErrors
{
    public static readonly Error ReviewAllowanceNotFound = new Error
        (404, "Review allowance of this id not found");

    public static readonly Error ReviewAllowanceNotGranted = new Error(
       400, "You can't publish review about this rental");
}
