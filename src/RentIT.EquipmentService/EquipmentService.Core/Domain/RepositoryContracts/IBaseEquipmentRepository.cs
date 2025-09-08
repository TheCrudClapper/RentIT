using EquipmentService.Core.Domain.Entities;
using System.Linq.Expressions;

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
        Task<IEnumerable<Equipment>> GetEquipmentsByCondition(Expression<Func<Equipment, bool>> expression);
        Task<Equipment?> GetEquipmentByCondition(Expression<Func<Equipment, bool>> expression);
    }
}
