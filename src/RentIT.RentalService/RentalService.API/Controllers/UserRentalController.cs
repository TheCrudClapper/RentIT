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

    // GET: api/UserRental/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.GetRental(id, CurrentUserId, cancellationToken));

    // PUT: api/UserRental/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRental(Guid id, UserRentalUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.UpdateRental(id, request, CurrentUserId, cancellationToken));

    // POST: api/UserRental
    [HttpPost]
    public async Task<ActionResult<RentalResponse>> PostRental(UserRentalAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.AddRental(request, CurrentUserId, cancellationToken));

    // DELETE: api/UserRental/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.DeleteRental(id, CurrentUserId, cancellationToken));

    // POST: api/UserRental/mark-equipment-as-returned/{id}
    [HttpPost("mark-equipment-as-returned/{id}")]
    public async Task<IActionResult> MarkEquipmentAsReturned(Guid id, UserReturnEquipmentRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.MarkEquipmentAsReturned(id, CurrentUserId, request, cancellationToken));
}
