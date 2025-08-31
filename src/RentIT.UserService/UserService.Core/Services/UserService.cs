using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Core.DTO.UserDto;
using UserService.Core.Mappings;
using UserService.Core.ResultTypes;
using UserService.Core.ServiceContracts;

namespace UserService.Core.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    public UserService(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        IEnumerable<User> users = await _userRepository.GetAllActiveUsersAsync();
        return users.Select(item => item.ToUserResponse());
    }

    public async Task<Result<UserDTO>> GetUserByUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure<UserDTO>(UserErrors.UserDoesNotExist);

        var roles = await _userManager.GetRolesAsync(user);
        if(!roles.Any())
            return Result.Failure<UserDTO>(RoleErrors.UserNotInRole);

        return user.ToUserDTO(roles);
    }
}

