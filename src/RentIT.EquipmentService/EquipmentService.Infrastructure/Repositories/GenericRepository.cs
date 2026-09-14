using EquipmentService.Core.Domain.Interfaces;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EquipmentService.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, ISoftDelete
{
    protected readonly EquipmentContext _context;
    
    public GenericRepository(EquipmentContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Set<T>().ToListAsync(ct);
    }

    public Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken ct = default, params Expression<Func<BaseEntity, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        foreach (var include in includes)
            query.Include(include);

        return query.FirstOrDefaultAsync(ct);
    }

    public void UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
