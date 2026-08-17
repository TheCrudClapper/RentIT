using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities.Role;
using UserService.Core.Domain.Entities.Role.Errors;
using UserService.Core.Domain.Entities.User;
using UserService.Core.Domain.Entities.User.Errors;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Core.Domain.ResultTypes;
using UserService.Core.DTO.Shared;
using UserService.Core.DTO.User;
using UserService.Core.DTO.UserDto;
using UserService.Core.Extensions;
using UserService.Core.Mappings;
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
        User? user = await _userManager.FindByIdAsync(userId.ToString());
        return user is null
            ? Result.Failure<UserDTO>(UserErrors.UserDoesNotExist)
            : user.ToUserDTO();
    }

    public async Task<Result<IReadOnlyCollection<UserDTO>>> GetUsersByUserIds(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<User> users = await _userRepository.GetUsersByCondition(item => userIds.Contains(item.Id), cancellationToken);
        return users.Select(u => u.ToUserDTO()).ToList();
    }

    public async Task<Result<CreatedResponse>> CreateUser(UserAddRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<CreatedResponse>(UserErrors.AlreadyExists);

        string requestRole = request.UserRoleType.ToString();
        if (!await _roleManager.RoleExistsAsync(requestRole))
            return Result.Failure<CreatedResponse>(RoleErrors.NotFound);

        User user = request.ToUserEntity();

        IdentityResult userResult = await _userManager.CreateAsync(user);
        if (!userResult.Succeeded)
            return userResult.ToResult<CreatedResponse>();

        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, requestRole);
        if(!roleResult.Succeeded)
            return roleResult.ToResult<CreatedResponse>();

        return user.ToCreatedResponse();
    }
}

