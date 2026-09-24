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

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Set<T>()
            .AnyAsync(x => x.Id == id, ct);
    }

    public async Task<bool> ExistsAsyncByCondition(Expression<Func<T, bool>> expresion, CancellationToken ct = default)
    {
        return await _context.Set<T>()
            .AnyAsync(expresion, ct);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsyncByCondition(Expression<Func<T, bool>> expression, CancellationToken ct = default, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>()
               .AsNoTracking()
               .Where(expression)
               .AsQueryable();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync(ct);
    }

    public Task<T?> GetByConditionAsync(Guid id, Expression<Func<T, bool>> expression, bool asNoTracking = false, CancellationToken ct = default, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>()
            .Where(expression)
            .AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken ct = default, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);

        return query.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public void UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
