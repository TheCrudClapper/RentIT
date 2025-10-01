using Microsoft.AspNetCore.Mvc;
using UserService.Core.DTO.UserDto;
using UserService.Core.ServiceContracts;
namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //POST :api/User/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);

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
        public async Task<ActionResult<UserAuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description,
                    statusCode: result.Error.ErrorCode);

            //return JWT Token
            return result.Value;
        }


    }
}
