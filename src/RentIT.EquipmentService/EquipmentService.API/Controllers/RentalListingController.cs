using EquipmentService.Core.DTO.RentalListings;
using EquipmentService.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/listings")]
[AllowAnonymous]
[ApiController]
public class RentalListingController : BaseApiController
{
    private readonly IRentalListingService _rentalListingService;
    public RentalListingController(IRentalListingService rentalListingService)
    {
        _rentalListingService = rentalListingService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RentalListingResponse>> GetByEquipmentId(Guid id, CancellationToken ct)
        => HandleResult(await _rentalListingService.GetRentalListing(id, ct));

    [HttpGet("equipment/{id}")]
    public async Task<ActionResult<RentalListingResponse>> GetById(Guid id, CancellationToken ct)
        => HandleResult(await _rentalListingService.GetRentalListingsByEquipmentId(id, ct));

    [HttpGet]
    public async Task<ActionResult<RentalListingResponse>> GetAll(CancellationToken ct)
        => HandleResult(await _rentalListingService.GetAllRentalListings(ct));
}
