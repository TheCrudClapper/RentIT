using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.Core.DTO.Rentals;
using RentalService.Core.ServiceContracts;

namespace RentalService.API.Controllers;

[Route("api/rentals")]
[Authorize]
[ApiController]
public class RentalsController : BaseApiController
{
    private readonly IRentalService _rentalService;
    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
        => HandleResult(await _rentalService.GetAllRentals(cancellationToken));

    [HttpGet("{equipmentId}")]
    [AllowAnonymous]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.GetRental(id, cancellationToken));

    [HttpPut("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutRental(Guid id, RentalUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.UpdateRental(id, request, cancellationToken));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RentalResponse>> PostRental(RentalAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.AddRental(request, cancellationToken));

    [HttpDelete("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.DeleteRental(id, cancellationToken));

    [HttpPost("{rentalId}/return")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkEquipmentAsReturned(Guid rentalId, ReturnEquipmentRequest request, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.MarkEquipmentAsReturned(rentalId, request, cancellationToken));
}
