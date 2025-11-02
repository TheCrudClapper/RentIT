using Microsoft.AspNetCore.Identity;
using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;


namespace UserService.Core.ServiceContracts;
/// <summary>
/// Defines methods for user authentication and registration operations within an application.
/// </summary>
/// <remarks>Implementations of this interface should handle user credential validation, account creation, and
/// related authentication workflows. Methods are asynchronous and support cancellation via the provided token. Thread
/// safety and error handling depend on the specific implementation.</remarks>

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
