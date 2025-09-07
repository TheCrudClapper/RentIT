using Microsoft.AspNetCore.Mvc;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ServiceContracts;

namespace RentalService.API.Controllers
{
    /// <summary>
    /// WORK IN PROGRESS - USER ID WILL BE TAKEN FROM JWT TOKEN
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserRentalController : ControllerBase
    {
        private readonly IUserRentalService _userRentalService;
        public readonly static Guid UserIdPlaceholder = new Guid();
        public UserRentalController(IUserRentalService userRentalService)
        {
            _userRentalService = userRentalService;
        }
        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals()
        {
            var rentals = await _userRentalService.GetAllRentals(UserIdPlaceholder);
            return rentals.ToList();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalResponse>> GetRental(Guid id)
        {
            var result = await _userRentalService.GetRental(id, UserIdPlaceholder);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return result.Value;
        }

        // PUT: api/Rentals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(Guid id, RentalUpdateRequest request)
        {
            var result = await _userRentalService.UpdateRental(id, request, UserIdPlaceholder);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        // POST: api/Rentals
        [HttpPost]
        public async Task<ActionResult<RentalResponse>> PostRental(UserRentalAddRequest request)
        {
            var result = await _userRentalService.AddRental(request, UserIdPlaceholder);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return result.Value;
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(Guid id)
        {
            var result = await _userRentalService.DeleteRental(id, UserIdPlaceholder);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        }
    }
}
