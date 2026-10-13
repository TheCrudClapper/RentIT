using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentIT.Contracts.DTO.Shared;
using RentIT.Contracts.DTO.UserMicroservice.User;
using UserService.Core.ServiceContracts;

namespace UserService.API.Controllers;

[Route("api/users")]
[Authorize]
[ApiController]
public class UsersController : BaseApiController
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
        => HandleResult(await _userService.GetUserByUserId(userId, cancellationToken));

    [HttpPost("query")]
    public async Task<ActionResult<IReadOnlyCollection<UserDTO>>> GetUsersByIds([FromBody] IEnumerable<Guid> userIds, CancellationToken cancellationToken)
        => HandleResult(await _userService.GetUsersByUserIds(userIds, cancellationToken));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CreatedResponse>> PostUser(UserAddRequest request)
        => HandleResult(await _userService.CreateUser(request));
}
