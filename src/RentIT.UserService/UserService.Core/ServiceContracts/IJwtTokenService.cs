using UserService.Core.Domain.Entities;

namespace UserService.Core.ServiceContracts;
/// <summary>
/// Defines methods for generating JSON Web Tokens (JWT) and refresh tokens for user authentication and authorization
/// scenarios.
/// </summary>
/// <remarks>Implementations of this interface typically use user information and assigned roles to create secure
/// JWTs for use in authentication flows. The generated tokens can be used to identify and authorize users in
/// distributed systems. Refresh tokens are intended to allow clients to obtain new JWTs without requiring the user to
/// re-authenticate. The specific format and claims included in the tokens depend on the implementation.</remarks>
public interface IJwtTokenService
{
    string GenerateJwtToken(User user, IList<string> roles);
    string GenerateJwtRefreshToken();
}
