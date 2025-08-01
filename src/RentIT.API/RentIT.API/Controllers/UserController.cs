using Microsoft.AspNetCore.Mvc;
using RentIT.Core.DTO.UserDto;
using RentIT.Core.ServiceContracts;

namespace RentIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
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
                foreach (var error in result.Errors)
                {
                   ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
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

        //POST :api/User/ChangePassword

    }
}
