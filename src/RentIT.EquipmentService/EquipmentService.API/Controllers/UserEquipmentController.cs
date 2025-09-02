using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ServiceContracts.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers
{
    /// <summary>
    /// WORK IN PROGRESS - USER ID WILL BE TAKEN FROM JWT TOKEN
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserEquipmentController : ControllerBase
    {
        public readonly static Guid UserIdPlaceholder = new Guid();
        private readonly IUserEquipmentService _userEquipmentService;
        public UserEquipmentController(IUserEquipmentService userEquipmentService)
        {
            _userEquipmentService = userEquipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetEquipmentItems()
        {
            var equipmentItems = await _userEquipmentService.GetAllUserEquipment(UserIdPlaceholder);
            return equipmentItems.ToList();
        }

        [HttpGet("{equipmentId}")]
        public async Task<ActionResult<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            var result = await _userEquipmentService.GetUserEquipment(UserIdPlaceholder,equipmentId);
            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return result.Value;
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentResponse>> PostEquipment(UserEquipmentAddRequest request)
        {
            var result = await _userEquipmentService.AddUserEquipment(UserIdPlaceholder, request);
            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return result.Value;
        }

        [HttpPut("{equipmentId}")]
        public async Task<IActionResult> PutEquipment(Guid equipmentId, EquipmentUpdateRequest request)
        {
            var result = await _userEquipmentService.UpdateUserEquipment(equipmentId, UserIdPlaceholder, request);
            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        [HttpDelete("{equipmentId}")]
        public async Task<IActionResult> DeleteEquipment(Guid equipmentId)
        {
            var result = await _userEquipmentService.DeleteUserEquipment(UserIdPlaceholder, equipmentId);
            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

            return NoContent();
        }
        
    }
}
