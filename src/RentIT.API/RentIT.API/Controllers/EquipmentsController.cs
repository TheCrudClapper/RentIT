using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.DTO.EquipmentDto;
using RentIT.Core.ResultTypes;
using RentIT.Core.ServiceContracts;
using RentIT.Infrastructure.DbContexts;

namespace RentIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEquipmentService _equipmentService;
        public EquipmentsController(ApplicationDbContext context, IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            _context = context;
        }

        // GET: api/Equipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetEquipmentItems()
        {
            var equipmentItems = await _equipmentService.GetAllEquipmentItems();
            return equipmentItems.ToList();
        }

        // GET: api/Equipments/5
        [HttpGet("{equipmentId}")]
        public async Task<ActionResult<EquipmentResponse>> GetEquipment(Guid equipmentId)
        {
            var result = await _equipmentService.GetEquipment(equipmentId);

            if(result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return result.Value;
        }

        // PUT: api/Equipments/5
        [HttpPut("{equipmentId}")]
        public async Task<IActionResult> PutEquipment(Guid equipmentId, EquipmentUpdateRequest equipment)
        {
            var result = await _equipmentService.UpdateEquipment(equipmentId, equipment);

            if( result.IsFailure )
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return NoContent();
        }

        // POST: api/Equipments
        [HttpPost]
        public async Task<ActionResult<EquipmentResponse>> PostEquipment(EquipmentAddRequest request)
        {
            var result = await _equipmentService.AddEquipment(request);

            if(result.IsFailure)
                return Problem(detail:  result.Error.Description, statusCode: result.Error.ErrorCode);

            return result.Value;
        }

        // DELETE: api/Equipments/5
        [HttpDelete("{equipmentId}")]
        public async Task<IActionResult> DeleteEquipment(Guid equipmentId)
        {
            var result = await _equipmentService.DeleteEquipment(equipmentId);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return NoContent();
        }

    }
}
