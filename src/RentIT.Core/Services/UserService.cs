using Microsoft.AspNetCore.Identity;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Core.DTO.UserDto;
using RentIT.Core.Enums;
using RentIT.Core.Mappings;
using RentIT.Core.ResultTypes;
using RentIT.Core.ServiceContracts;

namespace RentIT.Core.Services
{
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
        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            User user = request.ToUserEntity();

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return IdentityResult.Failed(new IdentityError { Code = "", Description = "User with this email already exists" });

            if (!IsAllowedRole(request.UserRoleOption))
                return IdentityResult.Failed(new IdentityError{ Code = "", Description = "This role cannot be assigned during registration"});

            string userRole = request.UserRoleOption.ToString();

            if (!await _roleManager.RoleExistsAsync(userRole))
            {
                var roleCreationResult = await CreateRole(request.UserRoleOption);
                if (!roleCreationResult.Succeeded)
                    return IdentityResult.Failed(new IdentityError { Code = "", Description = $"Failed to create {request.UserRoleOption}" });
            }

            var userCreationResult = await _userManager.CreateAsync(user, request.Password);

            if (!userCreationResult.Succeeded)
                return userCreationResult;

            var roleAssignResult = await _userManager.AddToRoleAsync(user, userRole);

            return roleAssignResult;
        }
        public async Task<Result> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return Result.Failure(UserErrors.UserDoesNotExist);

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure(UserErrors.WrongPassword);

            //generate and return JWT TOKEN
            //TO BE CHANGED
            return Result.Success(user);
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
}
