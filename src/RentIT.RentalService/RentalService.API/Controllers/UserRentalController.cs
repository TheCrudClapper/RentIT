using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.API.Extensions;
using RentalService.Core.DTO.RentalDto;
using RentalService.Core.ServiceContracts;

namespace RentalService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserRentalController : BaseApiController
{
    private readonly IUserRentalService _userRentalService;

    public UserRentalController(IUserRentalService userRentalService)
    {
        _userRentalService = userRentalService;
    }

    // GET: api/UserRental
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.GetAllRentals(CurrentUserId, cancellationToken));

    // GET: api/UserRental/{rentalId}
    [HttpGet("{rentalId}")]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid rentalId, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.GetRental(rentalId, CurrentUserId, cancellationToken));

    // PUT: api/UserRental/{rentalId}
    [HttpPut("{rentalId}")]
    public async Task<IActionResult> PutRental(Guid rentalId, UserRentalUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.UpdateRental(rentalId, request, CurrentUserId, cancellationToken));

    // POST: api/UserRental
    [HttpPost]
    public async Task<ActionResult<RentalResponse>> PostRental(UserRentalAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.AddRental(request, CurrentUserId, cancellationToken));

    // DELETE: api/UserRental/{rentalId}
    [HttpDelete("{rentalId}")]
    public async Task<IActionResult> DeleteRental(Guid rentalId, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.DeleteRental(rentalId, CurrentUserId, cancellationToken));

    // POST: api/UserRental/{rentalId}/return
    [HttpPost("{rentalId}/return")]
    public async Task<IActionResult> MarkEquipmentAsReturned(Guid rentalId, ReturnEquipmentRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.MarkEquipmentAsReturned(rentalId, CurrentUserId, request, cancellationToken));
}
