using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace UserService.Core.ServiceContracts;

public interface IUserService
{
    Task<Result<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken);
}

