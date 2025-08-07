using RentIT.Core.DTO.EquipmentDto;
using RentIT.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts
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
