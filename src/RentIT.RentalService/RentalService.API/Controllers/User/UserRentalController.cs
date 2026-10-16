using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalService.Core.ServiceContracts;
using RentIT.Contracts.DTO.RentalMicroservice.Rentals;
using RentIT.Contracts.DTO.Shared;

namespace RentalService.API.Controllers.User;

[Route("api/user/rentals")]
[Authorize]
[ApiController]
public class UserRentalController : BaseApiController
{
    private readonly IUserRentalService _userRentalService;

    public UserRentalController(IUserRentalService userRentalService)
    {
        _userRentalService = userRentalService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RentalResponse>>> GetAllRentals(CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.GetAllRentals(CurrentUserId, cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RentalResponse>> GetRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.GetRental(id, CurrentUserId, cancellationToken));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutRental(Guid id, UserRentalUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.UpdateRental(id, request, CurrentUserId, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<RentalResponse>> PostRental(UserRentalAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.AddRental(request, CurrentUserId, cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRental(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.DeleteRental(id, CurrentUserId, cancellationToken));

    [HttpPost("{rentalId}/return")]
    public async Task<IActionResult> MarkEquipmentAsReturned(Guid rentalId, ReturnEquipmentRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userRentalService.MarkEquipmentAsReturned(rentalId, CurrentUserId, request, cancellationToken));
}
