using Microsoft.AspNetCore.Identity;
using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace UserService.Core.ServiceContracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    }
}
