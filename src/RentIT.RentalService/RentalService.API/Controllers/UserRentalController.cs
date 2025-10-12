using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.API.Extensions;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ServiceContracts;

namespace RentalService.API.Controllers;

/// <summary>
/// WORK IN PROGRESS - USER ID WILL BE TAKEN FROM JWT TOKEN
/// FOR TIME BEING, USING GUID OF USER FROM DB SEEDER CLASS
/// </summary>
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserRentalController : ControllerBase
{
    private readonly IUserRentalService _userRentalService;
    public Guid CurrentUserId => this.GetLoggedUserId();
    public UserRentalController(IUserRentalService userRentalService)
    {
        _userRentalService = userRentalService;
    }

    // GET: api/Rentals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
    {
        var response = await _userRentalService.GetAllRentals(CurrentUserId, cancellationToken);
        if (response.IsFailure)
            return Problem(detail: response.Error.Description, statusCode: response.Error.StatusCode);

        return Ok(response.Value);
    }

    // GET: api/Rentals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userRentalService.GetRental(id, CurrentUserId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    // PUT: api/Rentals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRental(Guid id, UserRentalUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _userRentalService.UpdateRental(id, request, CurrentUserId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    // POST: api/Rentals
    [HttpPost]
    public async Task<ActionResult<RentalResponse>> PostRental(UserRentalAddRequest request, CancellationToken cancellationToken)
    {
        var result = await _userRentalService.AddRental(request, CurrentUserId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    // DELETE: api/Rentals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRental(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userRentalService.DeleteRental(id, CurrentUserId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    [HttpPost("mark-equipment-as-returned/{id}")]
    public async Task<IActionResult> MarkEquipmentAsReturned(Guid id, UserReturnEquipmentRequest request ,CancellationToken cancellationToken)
    {
        var result = await _userRentalService.MarkEquipmentAsReturned(id, CurrentUserId, request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }
}
