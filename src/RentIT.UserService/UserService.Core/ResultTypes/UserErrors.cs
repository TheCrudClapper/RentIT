namespace UserService.Core.ResultTypes;

public class UserErrors
{
    public static readonly Error UserDoesNotExist = new Error(
        400, "User of given email does not exists");

    public static readonly Error WrongPassword = new Error(
        400, "Password is invalid");

    public static readonly Error AccountAlreadyExists = new Error(
        400, "User with this email already exists");

    public static readonly Error FailedToCreateUser = new Error(
        400, "User with this email already exists");

}

