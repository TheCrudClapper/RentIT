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
        var result = await _authService.RegisterAsync(request, cancellationToken);
        return result.IsFailure
            ? Problem(title: "Error", detail: result.Error.Description)
            : NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserAuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);
        return result.IsFailure
            ? Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode)
            : Ok(result.Value);

    }
}
