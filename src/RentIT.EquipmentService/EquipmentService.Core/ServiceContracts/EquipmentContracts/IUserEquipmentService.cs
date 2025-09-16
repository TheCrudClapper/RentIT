using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts.Equipment
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserEquipmentService
    {
        Task<Result<EquipmentResponse>> AddUserEquipment(Guid userId, UserEquipmentAddRequest request);
        Task<Result> UpdateUserEquipment(Guid equipmentId, Guid userId, EquipmentUpdateRequest request);
        Task<IEnumerable<EquipmentResponse>> GetAllUserEquipment(Guid userId);
        Task<Result> DeleteUserEquipment(Guid userId, Guid equipmentId);
        Task<Result<EquipmentResponse>> GetUserEquipmentById(Guid userId, Guid equipmentId);
    }
}
