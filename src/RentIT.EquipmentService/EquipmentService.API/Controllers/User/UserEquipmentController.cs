using EquipmentService.API.DTO.Public;
using EquipmentService.API.Mappings;
using EquipmentService.Core.DTO.Equipments.User;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers.User;

[Route("api/user/equipments")]
[Authorize]
[ApiController]
public class UserEquipmentController : BaseApiController
{
    private readonly IUserEquipmentService _userEquipmentService;
    public UserEquipmentController(IUserEquipmentService userEquipmentService)
        => _userEquipmentService = userEquipmentService;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserEquipmentListItem>>> GetAllEquipments(CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.GetAllUserEquipment(CurrentUserId, cancellationToken));

    [HttpGet("{equipmentId:guid}")]
    public async Task<ActionResult<UserEquipmentResponse>> GetEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.GetUserEquipmentById(CurrentUserId, equipmentId, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostEquipment(UserEquipmentAddRequestExternal request)
        => HandleResult(await _userEquipmentService.AddUserEquipment(CurrentUserId, await request.Map()));

    [HttpPut("{equipmentId:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutEquipment(Guid equipmentId, UserEquipmentUpdateRequestExternal request)
        => HandleResult(await _userEquipmentService.UpdateUserEquipment(equipmentId, CurrentUserId, await request.Map()));

    [HttpDelete("{equipmentId:guid}")]
    public async Task<IActionResult> DeleteEquipment(Guid equipmentId)
        => HandleResult(await _userEquipmentService.DeleteUserEquipment(CurrentUserId, equipmentId));
}
