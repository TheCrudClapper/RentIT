using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.JsonWebTokens;
using RentIT.UI.Core.DTO.Auth;
using System.Security.Claims;

namespace RentIT.BlazorFrontend.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContext;
    public AuthenticationService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public async Task SignInAsync(UserAuthResponse tokenResponse)
    {
        JsonWebTokenHandler handler = new();
        JsonWebToken token = handler.ReadJsonWebToken(tokenResponse.Token);

        List<Claim> claims = token.Claims.ToList();
        ClaimsIdentity identity = new(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        ClaimsPrincipal principal = new(identity);

        await _httpContext.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);
    }

    public async Task SignOutAsync()
    {
        await _httpContext.HttpContext!.SignOutAsync();
    }
}
