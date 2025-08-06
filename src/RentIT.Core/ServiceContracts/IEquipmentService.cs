using RentIT.Core.DTO.EquipmentDto;
using RentIT.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts
{
    public interface IEquipmentService
    {
        Task<Result<EquipmentResponse>> GetActiveEquipment(Guid equipmentId);
        Task<IEnumerable<EquipmentResponse>> GetAllActiveEquipmentItems();
        Task<Result> DeleteEquipment(Guid equipmentId);
    }
}
