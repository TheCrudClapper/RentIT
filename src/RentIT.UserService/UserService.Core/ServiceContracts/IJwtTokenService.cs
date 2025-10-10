using UserService.Core.Domain.Entities;

namespace UserService.Core.ServiceContracts;
public interface IJwtTokenService
{
    string GenerateJwtToken(User user, IList<string> roles);
    string GenerateJwtRefreshToken();
}
