using EquipmentService.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EquipmentService.Core.Domain.RepositoryContracts;

/// <summary>
/// Interface that contains common methods for equipment repository and user equimpent repository
/// </summary>
public interface IBaseEquipmentRepository
{
    Task<bool> IsEquipmentUnique(Equipment equipment, CancellationToken cancellationToken, Guid? excludeId = null);
    Task<bool> DoesEquipmentExistsAsync(Guid equipmentId, CancellationToken cancellationToken);
    Task<IEnumerable<Equipment>> GetEquipmentsByCondition(Expression<Func<Equipment, bool>> expression, CancellationToken cancellationToken);
    Task<Equipment?> GetEquipmentByCondition(Expression<Func<Equipment, bool>> expression, CancellationToken cancellationToken);
}
