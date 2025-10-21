using EquipmentService.API.Extensions;
using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserEquipmentController : ControllerBase
{
    private readonly IUserEquipmentService _userEquipmentService;
    public Guid CurrentUserId => this.GetLoggedUserId();
    public UserEquipmentController(IUserEquipmentService userEquipmentService)
    {
        _userEquipmentService = userEquipmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetEquipmentItems(CancellationToken cancellationToken)
    {
        var equipmentItems = await _userEquipmentService.GetAllUserEquipment(CurrentUserId, cancellationToken);
        return Ok(equipmentItems);
    }

    [HttpGet("{equipmentId}")]
    public async Task<ActionResult<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        var result = await _userEquipmentService.GetUserEquipmentById(CurrentUserId, equipmentId, cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentResponse>> PostEquipment(UserEquipmentAddRequest request, CancellationToken cancellationToken)
    {
        var result = await _userEquipmentService.AddUserEquipment(CurrentUserId, request, cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    [HttpPut("{equipmentId}")]
    public async Task<IActionResult> PutEquipment(Guid equipmentId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _userEquipmentService.UpdateUserEquipment(equipmentId, CurrentUserId, request, cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    [HttpDelete("{equipmentId}")]
    public async Task<IActionResult> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        var result = await _userEquipmentService.DeleteUserEquipment(CurrentUserId, equipmentId, cancellationToken);
        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }
}
