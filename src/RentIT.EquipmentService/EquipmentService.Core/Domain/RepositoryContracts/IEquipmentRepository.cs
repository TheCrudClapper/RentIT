using EquipmentService.Core.Domain.Entities;
namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IEquipmentRepository
{
    Task<Equipment> AddUserEquipment(Equipment equipment, Guid userId);
    Task UpdateUserEquipmentAsync(Equipment equipment);
    Task<bool> IsEquipmentUnique(Equipment equipment, Guid? excludeId = null);
    Task<Equipment> AddEquipmentAsync(Equipment equipment);
    Task<bool> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment);
    Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid userId);
    Task<Equipment?> GetUserEquipmentByIdAsync(Guid equipmentId, Guid userId);
    Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId);
    Task<IEnumerable<Equipment>> GetAllEquipmentAsync();
    Task<bool> DeleteEquipmentAsync(Guid equipmentId);
    Task<bool> DeleteUserEquipmentAsync(Guid userId, Guid equipmentId);
    Task<decimal?> GetDailyPriceAsync(Guid equipmentId);
    Task<bool> DoesEquipmentExistsAsync(Guid equipmentId);
    Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId);
}

