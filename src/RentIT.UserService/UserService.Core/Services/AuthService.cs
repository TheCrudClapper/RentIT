using Microsoft.AspNetCore.Identity;
using UserService.Core.Domain.Entities.Role;
using UserService.Core.Domain.Entities.Role.Errors;
using UserService.Core.Domain.Entities.User;
using UserService.Core.Domain.Entities.User.Errors;
using UserService.Core.Domain.ResultTypes;
using UserService.Core.DTO.UserDto;
using UserService.Core.Extensions;
using UserService.Core.Mappings;
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

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return Result.Failure(UserErrors.UserAlreadyExists);

            string userRole = request.UserRoleType.ToString();
            if (!await _roleManager.RoleExistsAsync(userRole))
                return Result.Failure(RoleErrors.NotFound);

            User user = request.ToUserEntity();

            IdentityResult userCreationResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreationResult.Succeeded)
                return userCreationResult.ToResult();

            var roleAssignResult = await _userManager.AddToRoleAsync(user, userRole);

            if (!roleAssignResult.Succeeded)
                return roleAssignResult.ToResult();

            return Result.Success();
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
