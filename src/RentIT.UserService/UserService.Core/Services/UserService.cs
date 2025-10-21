using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;
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

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        IEnumerable<User> users = await _userRepository.GetAllUsersAsync(cancellationToken);

        return users
            .Select(item => item.ToUserResponse())
            .ToList();
    }

    public async Task<Result<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure<UserDTO>(UserErrors.UserDoesNotExist);

        var roles = await _userManager.GetRolesAsync(user);
        if(!roles.Any())
            return Result.Failure<UserDTO>(RoleErrors.UserNotInRole);

        return user.ToUserDTO(roles);
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByUserId(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default)
    {
        Expression<Func<User, bool>> expression = item => userIds.Contains(item.Id);
        var users = await  _userRepository.GetUsersByCondition(expression, cancellationToken);
        //IN FUTURE JUST DOWNLOAD USERS EMAIL INSTEAD
        List<UserDTO> results = new();
        foreach(var user in users)
        {
            var role = await _userManager.GetRolesAsync(user);
            results.Add(user.ToUserDTO(role));
        }

        return results;
    }
}

