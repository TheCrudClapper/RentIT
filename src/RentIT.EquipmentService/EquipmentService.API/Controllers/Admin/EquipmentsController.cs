using EquipmentService.Core.DTO.Equipments.Admin;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers.Admin;

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
    public async Task<ActionResult<UpdatedResponse>> PutEquipment(Guid equipmentId, EquipmentUpdateRequest request)
        => HandleResult(await _equipmentService.UpdateEquipment(equipmentId, request));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CreatedResponse>> PostEquipment(EquipmentAddRequest request)
        => HandleResult(await _equipmentService.AddEquipment(request));

    [HttpDelete("{equipmentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEquipment(Guid equipmentId)
        => HandleResult(await _equipmentService.DeleteEquipment(equipmentId));

    [HttpPost("query")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<EquipmentResponse>>> GetEquipmentsByIds([FromBody] IEnumerable<Guid>? equipmentIds, CancellationToken cancellationToken)
    {
        if (equipmentIds == null || !equipmentIds.Any())
            return BadRequest("No equipment IDs provided.");

        return HandleResult(await _equipmentService.GetAllEquipmentsByIds(equipmentIds, cancellationToken));
    }
}
