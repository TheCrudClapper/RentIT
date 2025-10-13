using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.API.Extensions;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ServiceContracts;
using StackExchange.Redis;

namespace RentalService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;
    private string Token => this.GetAuthorizationToken();
    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    // GET: api/Rentals
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
    {
        var response = await _rentalService.GetAllRentals(cancellationToken);
        if(response.IsFailure)
            return Problem(detail: response.Error.Description, statusCode: response.Error.StatusCode);

        return Ok(response.Value);
    }

    // GET: api/Rentals/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid id, CancellationToken cancellationToken)
    {
        var result = await _rentalService.GetRental(id, cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    // PUT: api/Rentals/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutRental(Guid id, RentalUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _rentalService.UpdateRental(id, request, Token, cancellationToken);

        if(result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    // POST: api/Rentals
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RentalResponse>> PostRental(RentalAddRequest request, CancellationToken cancellationToken)
    {
        var result = await _rentalService.AddRental(request, Token, cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;    
    }

    // DELETE: api/Rentals/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRental(Guid id, CancellationToken cancellationToken)
    {
        var result = await _rentalService.DeleteRental(id, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    [HttpDelete("by-equipment-id/{id}")]
    public async Task<IActionResult> DeleteRentalsByEquipmentId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _rentalService.DeleteRentalByEquipmentId(id, cancellationToken);

        if(result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    [HttpPost("mark-equipment-as-returned")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkEquipmentAsReturned(ReturnEquipmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _rentalService.MarkEquipmentAsReturned(request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }
}
