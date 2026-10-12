using RentIT.Contracts.DTO.UserMicroservice.User;

namespace RentIT.BlazorFrontend.Auth;

public interface ISessionAuthenticationService
{
    Task SignInAsync(UserAuthResponse tokenResponse);
    Task SignOutAsync();
}
