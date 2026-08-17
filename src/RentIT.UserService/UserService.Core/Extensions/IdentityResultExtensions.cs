using Microsoft.AspNetCore.Identity;
using UserService.Core.ResultTypes;

namespace UserService.Core.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
            return Result.Success();

        return Result.Failure(new Error(400, JoinErrorDescriptions(identityResult)));
    }

    public static Result<T> ToResult<T>(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
            throw new InvalidOperationException("Cannot create Result<T> without a value on success.");

        return Result.Failure<T>(new Error(400, JoinErrorDescriptions(identityResult)));
    }

    private static string JoinErrorDescriptions(IdentityResult identityResult)
    {
        return string.Join("; ", identityResult.Errors.Select(x => x.Description));
    }
}
