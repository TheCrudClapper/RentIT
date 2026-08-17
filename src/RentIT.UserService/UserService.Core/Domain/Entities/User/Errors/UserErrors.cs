using UserService.Core.Domain.ResultTypes;

namespace UserService.Core.Domain.Entities.User.Errors;

public class UserErrors
{
    public static readonly Error UserDoesNotExist = Error.Create(ErrorType.NotFound, "User.DoesNotExist", "User of given Id does not exists");

    public static readonly Error LoginFailed = Error.Create(ErrorType.Unauthorized, "User.LoginFailed", "Password or email is incorrect");

    public static readonly Error AlreadyExists = Error.Create(ErrorType.Conflict, "User.AlreadyExists", "User with this email already exists");

    public static readonly Error FailedToCreateUser = Error.Create(ErrorType.Unexpected, "User.FailedToCreate", "Failed to create user");

}

