using EquipmentService.Core.Domain.Entities;

namespace EquipmentService.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Interface that contains common methods for equipment repository and user equimpent repository
    /// </summary>
    public interface IBaseEquipmentRepository
    {
        Task<bool> IsEquipmentUnique(Equipment equipment, Guid? excludeId = null);
        Task<decimal?> GetDailyPriceAsync(Guid equipmentId);
        Task<bool> DoesEquipmentExistsAsync(Guid equipmentId);
        Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId);
    }
}
