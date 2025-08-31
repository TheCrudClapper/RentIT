using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities;
using UserService.Core.DTO.UserDto;
using UserService.Core.Enums;
using UserService.Core.Mappings;
using UserService.Core.ResultTypes;
using UserService.Core.ServiceContracts;

namespace UserService.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            User user = request.ToUserEntity();

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return IdentityResult.Failed(new IdentityError { Code = "AccountExists", Description = UserErrors.AccountAlreadyExists.Description });

            if (!IsAllowedRole(request.UserRoleOption))
                return IdentityResult.Failed(new IdentityError { Code = "InvalidRole", Description = RoleErrors.InvalidRole.Description });

            string userRole = request.UserRoleOption.ToString();

            if (!await _roleManager.RoleExistsAsync(userRole))
            {
                var roleCreationResult = await CreateRole(request.UserRoleOption);
                if (!roleCreationResult.Succeeded)
                    return roleCreationResult;
            }

            var userCreationResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreationResult.Succeeded)
                return userCreationResult;

            var roleAssignResult = await _userManager.AddToRoleAsync(user, userRole);

            if (!roleAssignResult.Succeeded)
                return roleAssignResult;

            return IdentityResult.Success;
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
    }
}
