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


    //TEST METHOD - NOT USED IN PROD
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        //I know it slow, just for testign
        IEnumerable<User> users = await _userRepository.GetAllUsersAsync(cancellationToken);

        IList<UserResponse> dtos = [];
        foreach (var user in users) 
        {
            var roleString = await _userManager.GetRolesAsync(user);
            dtos.Add(user.ToUserResponse(await _userManager.GetRolesAsync(user)));
        }

        return dtos;
    }

    public async Task<Result<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result.Failure<UserDTO>(UserErrors.UserDoesNotExist);

        return user.ToUserDTO();
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByUserId(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>> expression = item => userIds.Contains(item.Id);
        var users = await  _userRepository.GetUsersByCondition(expression, cancellationToken);
        return users.Select(u => u.ToUserDTO());
    }


}

