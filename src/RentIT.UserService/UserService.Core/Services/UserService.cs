using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Core.DTO.Shared;
using UserService.Core.DTO.User;
using UserService.Core.DTO.UserDto;
using UserService.Core.Mappings;
using UserService.Core.ResultTypes;
using UserService.Core.ServiceContracts;

namespace UserService.Core.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly RoleManager<Role> _roleManager;
    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRepository = userRepository;
    }

    public async Task<Result<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user is null 
            ? Result.Failure<UserDTO>(UserErrors.UserDoesNotExist) 
            : user.ToUserDTO();
    }

    public async Task<IReadOnlyCollection<UserDTO>> GetUsersByUserIds(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<User> users = await _userRepository.GetUsersByCondition(item => userIds.Contains(item.Id), cancellationToken);
        return users.Select(u => u.ToUserDTO()).ToList();
    }

    public async Task<Result<CreatedResponse>> CreateUser(UserAddRequest request)
    {
        //if (await _userManager.FindByEmailAsync(request.Email) is not null)
        //    return Result.Failure<CreatedResponse>(UserErrors.UserAlreadyExists);

        //if (!await _roleManager.RoleExistsAsync(request.UserRoleType.ToString()))
        //    return Result.Failure<CreatedResponse>(RoleErrors.RoleDoesNotExist);

        throw new NotImplementedException();
    }
}

