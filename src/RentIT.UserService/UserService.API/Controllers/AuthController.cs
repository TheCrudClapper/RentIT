using Microsoft.AspNetCore.Mvc;
using RentIT.Contracts.DTO.UserMicroservice.User;
using UserService.Core.ServiceContracts;
namespace UserService.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
        => _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        => HandleResult(await _authService.RegisterUserAsync(request, cancellationToken));

    [HttpPost("login")]
    public async Task<ActionResult<UserAuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
        => HandleResult(await _authService.LoginAsync(request, cancellationToken));
}
