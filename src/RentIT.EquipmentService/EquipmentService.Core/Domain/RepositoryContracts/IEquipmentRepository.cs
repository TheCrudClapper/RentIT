using EquipmentService.Core.Domain.Entities;
namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IEquipmentRepository
{
    Task<bool> IsEquipmentUnique(Equipment equipment, Guid? excludeId = null);
    Task<Equipment> AddEquipmentAsync(Equipment equipment);
    Task<bool> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment);
    Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid equipmentId, Guid userId);
    Task<Equipment?> GetUserActiveEquipmentByIdAsync(Guid equipmentId, Guid userId);
    Task<Equipment?> GetActiveEquipmentByIdAsync(Guid equipmentId);
    Task<IEnumerable<Equipment>> GetAllActiveEquipmentItemsAsync();
    Task<bool> DeleteEquipmentAsync(Guid equipmentId);
    Task<decimal?> GetDailyPriceAsync(Guid equipmentId);
    Task<bool> DoesEquipmentExistsAsync(Guid equipmentId);
    Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId);
    Task<bool> DoesEquipmentBelongsToUser(Guid equipmentId, Guid userId);
}

