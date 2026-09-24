using EquipmentService.Core.DTO.RentalListings;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers.User;

[Route("api/user/listings")]
[Authorize]
[ApiController]
public class UserRentalListingController : BaseApiController
{
    private readonly IUserRentalListingService _service;
    public UserRentalListingController(IUserRentalListingService service) 
        => _service = service;

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostRentalListing(RentalListingAddRequest request)
        => HandleResult(await _service.AddUserRentalListing(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutRentalListing(Guid id, RentalListingUpdateRequest request)
        => HandleResult(await _service.UpdateUserRentalListing(id, CurrentUserId, request));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteListing(Guid id)
        => HandleResult(await _service.DeleteUserRentalListing(id, CurrentUserId));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RentalListingResponse>> GetListing(Guid id, CancellationToken ct)
        => HandleResult(await _service.GetUserRentalListingById(id, CurrentUserId, ct));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RentalListingResponse>>> GetUserListings(Guid id, CancellationToken ct)
       => HandleResult(await _service.GetAllUserRentalListings(id, ct));

}
