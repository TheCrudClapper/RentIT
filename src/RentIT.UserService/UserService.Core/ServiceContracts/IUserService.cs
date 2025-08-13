using Microsoft.AspNetCore.Identity;
using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace UserService.Core.ServiceContracts;

public interface IUserService
{
    Task<Result<UserAuthResponse>> RegisterAsync(RegisterRequest request);
    Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request);
    Task<IEnumerable<UserResponse>> GetAllActiveUsersAsync();
}

