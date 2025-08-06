using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.DTO.RentalDto;
using RentIT.Core.ServiceContracts;
using RentIT.Infrastructure.DbContexts;

namespace RentIT.API.Controllers
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
            var rentals = await _rentalService.GetAllActiveRentals();
            return rentals.ToList();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalResponse>> GetRental(Guid id)
        {
            var result = await _rentalService.GetActiveRental(id);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return result.Value;
        }

        // PUT: api/Rentals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(Guid id, RentalUpdateRequest request)
        {
            var result = await _rentalService.UpdateRental(id, request);

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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(Guid id)
        {
            var result = await _rentalService.DeleteRental(id);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return NoContent();
        }
    }
}
