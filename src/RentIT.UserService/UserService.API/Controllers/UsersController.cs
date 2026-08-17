using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.DTO.UserDto;
using UserService.Core.ServiceContracts;

namespace UserService.API.Controllers;

[Route("api/users")]
[Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserByUserId(userId, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error.Description);

        return result.Value;
    }

    [HttpPost("query")]
    public async Task<ActionResult<IReadOnlyCollection<UserDTO>>> GetUsersByIds([FromBody]IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersByUserIds(userIds, cancellationToken);
        return users.ToList();
    }
}
