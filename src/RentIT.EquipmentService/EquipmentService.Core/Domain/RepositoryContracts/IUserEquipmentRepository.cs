using EquipmentService.Core.Domain.Entities;

namespace EquipmentService.Core.Domain.RepositoryContracts
{
    public interface IUserEquipmentRepository : IBaseEquipmentRepository
    {
        Task<Equipment> AddUserEquipment(Equipment equipment, Guid userId);
        Task<bool> UpdateUserEquipmentAsync(Guid equipmentId, Equipment equipment);
        Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid userId);
        Task<Equipment?> GetUserEquipmentByIdAsync(Guid equipmentId, Guid userId);
        Task DeleteUserEquipmentAsync(Equipment equipment);
    }
}
