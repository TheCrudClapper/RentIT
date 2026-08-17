using EquipmentService.Core.DTO.Equipments;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/users/equipments")]
[Authorize]
[ApiController]
public class UserEquipmentController : BaseApiController
{
    private readonly IUserEquipmentService _userEquipmentService;
    public UserEquipmentController(IUserEquipmentService userEquipmentService)
        => _userEquipmentService = userEquipmentService;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<EquipmentResponse>>> GetAllUserEquipments(CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.GetAllUserEquipment(CurrentUserId, cancellationToken));

    [HttpGet("{equipmentId}")]
    public async Task<ActionResult<EquipmentResponse>> GetUserEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.GetUserEquipmentById(CurrentUserId, equipmentId, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<EquipmentResponse>> PostUserEquipment(UserEquipmentAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.AddUserEquipment(CurrentUserId, request, cancellationToken));

    [HttpPut("{equipmentId}")]
    public async Task<IActionResult> PutUserEquipment(Guid equipmentId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.UpdateUserEquipment(equipmentId, CurrentUserId, request, cancellationToken));

    [HttpDelete("{equipmentId}")]
    public async Task<IActionResult> DeleteUserEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.DeleteUserEquipment(CurrentUserId, equipmentId, cancellationToken));
}
