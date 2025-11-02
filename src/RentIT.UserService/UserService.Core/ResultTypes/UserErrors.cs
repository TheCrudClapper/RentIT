namespace UserService.Core.ResultTypes;

public class UserErrors
{
    public static readonly Error UserDoesNotExist = new Error(
        400, "User of given Id does not exists");

    public static readonly Error LoginFailed = new Error(
        400, "Password or email is incorrect");

    public static readonly Error AccountAlreadyExists = new Error(
        400, "User with this email already exists");

    public static readonly Error FailedToCreateUser = new Error(
        400, "Failed to create user");

}

