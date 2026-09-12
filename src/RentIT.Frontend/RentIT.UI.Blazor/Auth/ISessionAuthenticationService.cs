using RentIT.UI.Core.DTO.Auth;

namespace RentIT.BlazorFrontend.Auth;

public interface ISessionAuthenticationService
{
    Task SignInAsync(UserAuthResponse tokenResponse);
    Task SignOutAsync();
}
