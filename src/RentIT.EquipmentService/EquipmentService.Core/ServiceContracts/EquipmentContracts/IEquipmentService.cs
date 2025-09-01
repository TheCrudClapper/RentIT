using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts.Equipment
{
    public interface IEquipmentService
    {
        
        Task<Result> UpdateEquipment(Guid equipmentId,EquipmentUpdateRequest request);
        Task<Result<EquipmentResponse>> AddEquipment(EquipmentAddRequest request);
        Task<Result<EquipmentResponse>> GetEquipment(Guid equipmentId);
        Task<IEnumerable<EquipmentResponse>> GetAllEquipmentItems();
        Task<Result> DeleteEquipment(Guid equipmentId);
    }
}
