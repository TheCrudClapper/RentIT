using RentIT.UI.Core.DTO.Auth;

namespace RentIT.BlazorFrontend.Auth;

public interface IAuthenticationService
{
    Task SignInAsync(UserAuthResponse tokenResponse);
    Task SignOutAsync();
}
