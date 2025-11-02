using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ServiceContracts;
namespace RentalService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class RentalsController : BaseApiController
{
    private readonly IRentalService _rentalService;
    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    // GET: api/Rentals
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
        => HandleResult(await _rentalService.GetAllRentals(cancellationToken));

    // GET: api/Rentals/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.GetRental(id, cancellationToken));


    // PUT: api/Rentals/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutRental(Guid id, RentalUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.UpdateRental(id, request, cancellationToken));

    // POST: api/Rentals
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<RentalResponse>> PostRental(RentalAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.AddRental(request, cancellationToken));


    // DELETE: api/Rentals/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.DeleteRental(id, cancellationToken));


    // DELETE: api/Rentals/by-equipment-id/{id}
    [HttpDelete("by-equipment-id/{id}")]
    public async Task<IActionResult> DeleteRentalsByEquipmentId(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.DeleteRentalByEquipmentId(id, cancellationToken));


    // POST: api/Rentals/mark-equipment-as-returned
    [HttpPost("mark-equipment-as-returned")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkEquipmentAsReturned(ReturnEquipmentRequest request, CancellationToken cancellationToken)
        => HandleResult(await _rentalService.MarkEquipmentAsReturned(request, cancellationToken));
}
