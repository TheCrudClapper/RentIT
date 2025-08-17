using Microsoft.AspNetCore.Mvc;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ServiceContracts;

namespace RentalService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals()
        {
            var rentals = await _rentalService.GetAllRentals();
            return rentals.ToList();
        }

        // GET: api/Rentals/5
        [HttpGet("{rentalId}")]
        public async Task<ActionResult<RentalResponse>> GetRental(Guid rentalId)
        {
            var result = await _rentalService.GetRental(id);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return result.Value;
        }

        // PUT: api/Rentals/5
        [HttpPut("{rentalId}")]
        public async Task<IActionResult> PutRental(Guid rentalId, RentalUpdateRequest request)
        {
            var result = await _rentalService.UpdateRental(rentalId, request);

            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return NoContent();
        }

        // POST: api/Rentals
        [HttpPost]
        public async Task<ActionResult<RentalResponse>> PostRental(RentalAddRequest request)
        {
            var result = await _rentalService.AddRental(request);
            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return result.Value;    
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{rentalId}")]
        public async Task<IActionResult> DeleteRental(Guid rentalId)
        {
            var result = await _rentalService.DeleteRental(rentalId);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return NoContent();
        }
    }
}
