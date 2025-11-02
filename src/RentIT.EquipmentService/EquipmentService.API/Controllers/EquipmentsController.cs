using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EquipmentsController : BaseApiController
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentsController(IEquipmentService equipmentService)
        => _equipmentService = equipmentService;

    // GET: api/Equipments
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetAllEquipments(CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.GetAllEquipmentItems(cancellationToken));

    // GET: api/Equipments/{equipmentId}
    [HttpGet("{equipmentId}")]
    [AllowAnonymous]
    public async Task<ActionResult<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.GetEquipment(equipmentId, cancellationToken));

    // PUT: api/Equipments/{equipmentId}
    [HttpPut("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutEquipment(Guid equipmentId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.UpdateEquipment(equipmentId, request, cancellationToken));

    // POST: api/Equipments
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<EquipmentResponse>> PostEquipment(EquipmentAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.AddEquipment(request, cancellationToken));

    // DELETE: api/Equipments/{equipmentId}
    [HttpDelete("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.DeleteEquipment(equipmentId, cancellationToken));

    // GET: api/Equipments/exists/{equipmentId}
    [HttpGet("exists/{equipmentId}")]
    [AllowAnonymous]
    public async Task<ActionResult> DoesEquipmentExist(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.DoesEquipmentExist(equipmentId, cancellationToken));

    // POST: api/Equipments/byIds
    [HttpPost("byIds")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetEquipmentsByIds([FromBody] IEnumerable<Guid>? equipmentIds, CancellationToken cancellationToken)
    {
        if (equipmentIds == null || !equipmentIds.Any())
            return BadRequest("No equipment IDs provided.");

        return HandleResult(await _equipmentService.GetAllEquipmentsByIds(equipmentIds, cancellationToken));
    }
}
