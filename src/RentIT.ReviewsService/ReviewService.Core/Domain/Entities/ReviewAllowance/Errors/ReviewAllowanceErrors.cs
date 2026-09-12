using ReviewService.Core.Domain.ResultTypes;

namespace ReviewService.Core.Domain.Entities.ReviewAllowance.Errors;

public static class ReviewAllowanceErrors
{
    public static Error NotFound
        => new(ErrorType.NotFound, "ReviewAllowance.NotFound", "Review allowance not found.");

    public static Error ReviewAllowanceNotGranted
        => new(ErrorType.Validation, "ReviewAllowance.NotGranted", "You can't publish review about this rental.");
}
