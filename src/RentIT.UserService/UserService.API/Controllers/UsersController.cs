using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.DTO.UserDto;
using UserService.Core.ServiceContracts;

namespace UserService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    //GET :/api/Users/5
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDTO>> GetUserByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserByUserId(userId, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error.Description);

        return result.Value;
    }

    [HttpPost("byIds")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByIds([FromBody]IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        var users = await _userService.GetUsersByUserId(userIds, cancellationToken);
        return users.ToList();
    }

    //GET :/api/Users
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        return users.ToList();
    }
}
