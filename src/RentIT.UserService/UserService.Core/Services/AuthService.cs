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
        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
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
        public async Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return Result.Failure<UserAuthResponse>(UserErrors.UserDoesNotExist);

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure<UserAuthResponse>(UserErrors.WrongPassword);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateJwtToken(user, roles);

            return new UserAuthResponse(token);
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
