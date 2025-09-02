using EquipmentService.Core.Domain.Entities;

namespace EquipmentService.Core.Domain.RepositoryContracts
{
    public interface IUserEquipmentRepository : IBaseEquipmentRepository
    {
        Task<Equipment> AddUserEquipment(Equipment equipment, Guid userId);
        Task UpdateUserEquipmentAsync(Equipment equipment);
        Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid userId);
        Task<Equipment?> GetUserEquipmentByIdAsync(Guid equipmentId, Guid userId);
        Task<bool> DeleteUserEquipmentAsync(Guid userId, Guid equipmentId);
    }
}
