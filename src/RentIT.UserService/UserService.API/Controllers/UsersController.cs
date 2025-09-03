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

        //GET :/api/Users/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(Guid? userId)
        {
            if(userId == null)
                return BadRequest("User Id can't be null");

            var result = await _userService.GetUserByUserId(userId.Value);

            if (result.IsFailure)
                return NotFound(result.Error.Description);

            return result.Value;
        }

        //GET :/api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users.ToList();
        }
    }
}
