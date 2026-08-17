using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities;
using UserService.Core.DTO.UserDto;
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

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return IdentityResult.Failed(new IdentityError { Code = "AccountExists", Description = UserErrors.UserAlreadyExists.Description });

            string userRole = request.UserRoleType.ToString();
            if (!await _roleManager.RoleExistsAsync(userRole))
                return IdentityResult.Failed(new IdentityError { Code = "RoleNotExists", Description = "Role does not exists." });

            User user = request.ToUserEntity();

            IdentityResult userCreationResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreationResult.Succeeded)
                return userCreationResult;

            var roleAssignResult = await _userManager.AddToRoleAsync(user, userRole);

            if (!roleAssignResult.Succeeded)
                return roleAssignResult;

            return IdentityResult.Success;
        }

        public async Task<Result<UserAuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return Result.Failure<UserAuthResponse>(UserErrors.UserDoesNotExist);

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure<UserAuthResponse>(UserErrors.LoginFailed);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new UserAuthResponse(_jwtTokenService.GenerateJwtToken(user, roles));
        }
    }
}
