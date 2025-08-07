using RentIT.Core.Domain.Entities;
namespace RentIT.Core.Domain.RepositoryContracts
{
    public interface IEquipmentRepository
    {
        Task<Equipment> AddEquipmentAsync(Equipment equipment);
        Task<bool> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment);
        Task<Equipment?> GetActiveEquipmentByIdAsync(Guid equipmentId);
        Task<IEnumerable<Equipment>> GetAllActiveEquipmentItemsAsync();
        Task<bool> DeleteEquipmentAsync(Guid equipmentId);
        Task<decimal?> GetDailyPriceAsync(Guid equipmentId);
        Task<bool> DoesEquipmentExistsAsync(Guid equipmentId);
        Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId);
        Task<bool> DoesEquipmentBelongsToUser(Guid equipmentId, Guid userId);
    }
}
