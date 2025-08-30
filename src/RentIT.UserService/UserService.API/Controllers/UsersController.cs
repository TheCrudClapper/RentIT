using Microsoft.AspNetCore.Mvc;
using UserService.Core.DTO.UserDto;
using UserService.Core.ServiceContracts;
namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //POST :api/User/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                var errors = result.Errors
               .GroupBy(e => e.Code)
               .ToDictionary(
                   g => g.Key,
                   g => g.Select(e => e.Description).ToArray()
               );

                return ValidationProblem(new ValidationProblemDetails(errors));
            }

            return NoContent();
        }

        //POST :api/User/Login
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userService.LoginAsync(request);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description,
                    statusCode: result.Error.ErrorCode);

            //return JWT Token
            return NoContent();
        }

        //GET :/api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var users = await _userService.GetAllActiveUsersAsync();
            return users.ToList();
        }
    }
}
