using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities;
using UserService.Core.Domain.RepositoryContracts;
using UserService.Core.DTO.UserDto;
using UserService.Core.Enums;
using UserService.Core.Mappings;
using UserService.Core.ResultTypes;
using UserService.Core.ServiceContracts;

namespace UserService.Core.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IUserRepository _userRepository;
    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRepository = userRepository;
    }
    public async Task<Result<UserAuthResponse>> RegisterAsync(RegisterRequest request)
    {
        User user = request.ToUserEntity();

        if (await _userManager.FindByEmailAsync(request.Email) != null)
            return Result.Failure<UserAuthResponse>(UserErrors.AccountAlreadyExists);

        if (!IsAllowedRole(request.UserRoleOption))
            return Result.Failure<UserAuthResponse>(RoleErrors.InvalidRole);

        string userRole = request.UserRoleOption.ToString();

        if (!await _roleManager.RoleExistsAsync(userRole))
        {
            var roleCreationResult = await CreateRole(request.UserRoleOption);
            if (!roleCreationResult.Succeeded)
                return Result.Failure<UserAuthResponse>(RoleErrors.RoleCreationFailed);
        }

        var userCreationResult = await _userManager.CreateAsync(user, request.Password);

        if (!userCreationResult.Succeeded)
            return Result.Failure<UserAuthResponse>(UserErrors.FailedToCreateUser);

        var roleAssignResult = await _userManager.AddToRoleAsync(user, userRole);

        if(!roleAssignResult.Succeeded)
            return Result.Failure<UserAuthResponse>(RoleErrors.RoleAssignationFailed);

        //generate JWT TOKEN
        //put creds into response
        return new UserAuthResponse(user.Id, user.Email!, "token");
    }
    public async Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Failure<UserAuthResponse>(UserErrors.UserDoesNotExist);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return Result.Failure<UserAuthResponse>(UserErrors.WrongPassword);

        //generate and return JWT TOKEN
        //TO BE CHANGED

        //fill token later
        return new UserAuthResponse(user.Id, user.Email!, "token");
    }

    private async Task<IdentityResult> CreateRole(UserRoleOption roleOption)
    {
        Role role = roleOption.ToRoleEntity();
        return await _roleManager.CreateAsync(role);
    }

    private bool IsAllowedRole(UserRoleOption role) =>
            role == UserRoleOption.User || role == UserRoleOption.Admin;

    public async Task<IEnumerable<UserResponse>> GetAllActiveUsersAsync()
    {
        IEnumerable<User> users = await _userRepository.GetAllActiveUsersAsync();
        return users.Select(item => item.ToUserResponse());
    }

}

