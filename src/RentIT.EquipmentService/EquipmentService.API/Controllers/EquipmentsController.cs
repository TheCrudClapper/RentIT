using EquipmentService.Core.DTO.Equipments;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/equipments")]
[Authorize]
[ApiController]
public class EquipmentsController : BaseApiController
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentsController(IEquipmentService equipmentService)
        => _equipmentService = equipmentService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<EquipmentResponse>>> GetAllEquipments(CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.GetAllEquipmentItems(cancellationToken));

    [HttpGet("{equipmentId}")]
    [AllowAnonymous]
    public async Task<ActionResult<EquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.GetEquipment(equipmentId, cancellationToken));

    [HttpPut("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutEquipment(Guid equipmentId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.UpdateEquipment(equipmentId, request, cancellationToken));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<EquipmentResponse>> PostEquipment(EquipmentAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.AddEquipment(request, cancellationToken));

    [HttpDelete("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _equipmentService.DeleteEquipment(equipmentId, cancellationToken));

    [HttpPost("query")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<EquipmentResponse>>> GetEquipmentsByIds([FromBody] IEnumerable<Guid>? equipmentIds, CancellationToken cancellationToken)
    {
        if (equipmentIds == null || !equipmentIds.Any())
            return BadRequest("No equipment IDs provided.");

        return HandleResult(await _equipmentService.GetAllEquipmentsByIds(equipmentIds, cancellationToken));
    }
}
