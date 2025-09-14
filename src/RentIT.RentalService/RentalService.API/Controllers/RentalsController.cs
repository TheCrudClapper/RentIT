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
            var response = await _rentalService.GetAllRentals();
            if(response.IsFailure)
                return Problem(detail: response.Error.Description, statusCode: response.Error.StatusCode);

            return Ok(response.Value);
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalResponse>> GetRental(Guid id)
        {
            var result = await _rentalService.GetRental(id);
            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return result.Value;
        }

        // PUT: api/Rentals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(Guid id, RentalUpdateRequest request)
        {
            var result = await _rentalService.UpdateRental(id, request);

            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        // POST: api/Rentals
        [HttpPost]
        public async Task<ActionResult<RentalResponse>> PostRental(RentalAddRequest request)
        {
            var result = await _rentalService.AddRental(request);
            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return result.Value;    
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(Guid id)
        {
            var result = await _rentalService.DeleteRental(id);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        [HttpDelete("byEquipmentId/{id}")]
        public async Task<IActionResult> DeleteRentalsByEquipmentId(Guid id)
        {
            var result = await _rentalService.DeleteRentalByEquipmentId(id);

            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        } 
    }
}
