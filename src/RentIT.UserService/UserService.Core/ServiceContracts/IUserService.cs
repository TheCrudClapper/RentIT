using UserService.Core.DTO.UserDto;
using UserService.Core.ResultTypes;

namespace UserService.Core.ServiceContracts;

public interface IUserService
{
    Task<Result<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<UserDTO>> GetUsersByUserId(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
}

