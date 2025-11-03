using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserEquipmentController : BaseApiController
{
    private readonly IUserEquipmentService _userEquipmentService;
    public UserEquipmentController(IUserEquipmentService userEquipmentService)
        => _userEquipmentService = userEquipmentService;

    // GET: api/UserEquipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetAllUserEquipments(CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.GetAllUserEquipment(CurrentUserId, cancellationToken));

    // GET: api/UserEquipment/{equipmentId}
    [HttpGet("{equipmentId}")]
    public async Task<ActionResult<EquipmentResponse>> GetUserEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.GetUserEquipmentById(CurrentUserId, equipmentId, cancellationToken));

    // POST: api/UserEquipment
    [HttpPost]
    public async Task<ActionResult<EquipmentResponse>> PostUserEquipment(UserEquipmentAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.AddUserEquipment(CurrentUserId, request, cancellationToken));

    // PUT: api/UserEquipment/{equipmentId}
    [HttpPut("{equipmentId}")]
    public async Task<IActionResult> PutUserEquipment(Guid equipmentId, EquipmentUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.UpdateUserEquipment(equipmentId, CurrentUserId, request, cancellationToken));

    // DELETE: api/UserEquipment/{equipmentId}
    [HttpDelete("{equipmentId}")]
    public async Task<IActionResult> DeleteUserEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _userEquipmentService.DeleteUserEquipment(CurrentUserId, equipmentId, cancellationToken));
}
