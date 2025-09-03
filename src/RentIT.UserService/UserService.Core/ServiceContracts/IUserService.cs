using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace UserService.Core.ServiceContracts;

public interface IUserService
{
    Task<Result<UserDTO>> GetUserByUserId(Guid userId);
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
}

