using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EquipmentService.Infrastructure.Repositories;

public abstract class BaseEquipmentRepository : IBaseEquipmentRepository
{
    protected readonly EquipmentContext _context;
    protected BaseEquipmentRepository(EquipmentContext context)
    {
        _context = context;
    }
    public async Task<bool> DoesEquipmentExistsAsync(Guid equipmentId, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .AnyAsync(item => item.Id == equipmentId, cancellationToken);
    }

    public async Task<decimal?> GetDailyPriceAsync(Guid equipmentId, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .Where(item => item.Id == equipmentId && item.IsActive)
            .Select(item => item.RentalPricePerDay)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Equipment?> GetEquipmentByCondition(Expression<Func<Equipment, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByCondition(Expression<Func<Equipment, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .Include(item => item.Category)
            .Where(expression)
            .ToListAsync(cancellationToken);
    }

    public async  Task<bool> IsEquipmentUnique(Equipment equipment, CancellationToken cancellationToken, Guid? excludeId = null)
    {
        return !await _context.EquipmentItems
            .AnyAsync(item => (item.Name == equipment.Name || item.SerialNumber == equipment.SerialNumber
            && (excludeId == null || item.Id != excludeId)), cancellationToken);
    }

}
