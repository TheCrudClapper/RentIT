using Microsoft.AspNetCore.Identity;
using RentIT.Core.DTO.UserDto;
using RentIT.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request);
        Task<Result> LoginAsync(LoginRequest request);
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();

        Task<bool> DoesUserExists(Guid userId);
    }
}
