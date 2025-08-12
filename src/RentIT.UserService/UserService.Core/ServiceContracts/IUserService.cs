using Microsoft.AspNetCore.Identity;
using UserService.Core.DTO;
using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts;

public interface IUserService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<Result> LoginAsync(LoginRequest request);
    Task<IEnumerable<UserResponse>> GetAllActiveUsersAsync();
}

