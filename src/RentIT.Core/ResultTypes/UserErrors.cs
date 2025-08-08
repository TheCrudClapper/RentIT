namespace RentIT.Core.ResultTypes
{
    public class UserErrors
    {
        public static readonly Error UserNotFound = new Error(
            400, "User of given Id does not exists");

        public static readonly Error WrongPassword = new Error(
            400, "Password is invalid");
    }
}
