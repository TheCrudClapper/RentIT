using EquipmentService.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyCollection<T>> GetAllAsyncByCondition(Expression<Func<T, bool>> expression, CancellationToken ct = default, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken ct = default, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByConditionAsync(Expression<Func<T, bool>> expression, bool asNoTracking = false, CancellationToken ct = default, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
}