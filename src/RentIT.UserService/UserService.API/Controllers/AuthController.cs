using Microsoft.AspNetCore.Mvc;
using UserService.Core.DTO.UserDto;
using UserService.Core.ServiceContracts;
namespace UserService.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) 
        => _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterUserAsync(request, cancellationToken);

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

    [HttpPost("login")]
    public async Task<ActionResult<UserAuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description,
                statusCode: result.Error.ErrorCode);

        return result.Value;
    }
}
