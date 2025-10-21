using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EquipmentsController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;
    public EquipmentsController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    // GET: api/Equipments
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetEquipmentItems(CancellationToken cancellationToken)
    {
        var equipmentItems = await _equipmentService.GetAllEquipmentItems(cancellationToken);
        return Ok(equipmentItems);
    }

    // GET: api/Equipments/5
    [HttpGet("{equipmentId}")]
    [AllowAnonymous]
    public async Task<ActionResult<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        var result = await _equipmentService.GetEquipment(equipmentId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    // PUT: api/Equipments/5
    [HttpPut("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutEquipment(Guid equipmentId, EquipmentUpdateRequest equipment, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.UpdateEquipment(equipmentId, equipment, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    // POST: api/Equipments
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<EquipmentResponse>> PostEquipment(EquipmentAddRequest request, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.AddEquipment(request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    // DELETE: api/Equipments/5
    [HttpDelete("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.DeleteEquipment(equipmentId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    // GET: api/Equipments/exists/5
    [HttpGet("exists/{equipmentId}")]
    public async Task<ActionResult> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.DoesEquipmentExist(equipmentId, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error.Description);

        return Ok(result.Value);
    }

    //POST: api/Equipments/byIds
    [HttpPost("byIds")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetEquipmentItems([FromBody]IEnumerable<Guid>? equipmentIds, CancellationToken cancellationToken)
    {
        if (equipmentIds == null || !equipmentIds.Any())
            return BadRequest("No equipment IDs provided.");

        var equipments = await _equipmentService.GetAllEquipmentsByIds(equipmentIds, cancellationToken);
        return Ok(equipments);
    }
}

