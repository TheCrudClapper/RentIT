using RentIT.Core.Domain.Entities;
using RentIT.Core.DTO.EquipmentDto;

namespace RentIT.Core.Mappings
{
    public static class EquipmentMappings
    {
        public static EquipmentResponse ToEquipmentResponse(this Equipment equipment)
        {
            return new EquipmentResponse
            {
                Id = equipment.Id,
                Category = equipment.Category.Name,
                CreatedBy = equipment.CreatedBy.FirstName + " " + equipment.CreatedBy.LastName,
                Notes = equipment.Notes,
                SerialNumber = equipment.SerialNumber,
                Status = equipment.Status.ToString(),
            }; 
        }
    }
}
