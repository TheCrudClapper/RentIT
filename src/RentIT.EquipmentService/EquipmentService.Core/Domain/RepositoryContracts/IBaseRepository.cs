using EquipmentService.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken ct = default, params Expression<Func<BaseEntity, object>>[] includes);
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
}