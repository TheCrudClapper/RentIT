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
    public async Task<bool> DoesEquipmentExistsAsync(Guid equipmentId)
    {
        return await _context.EquipmentItems
            .AnyAsync(item => item.Id == equipmentId);
    }

    public async Task<decimal?> GetDailyPriceAsync(Guid equipmentId)
    {
        return await _context.EquipmentItems
            .Where(item => item.Id == equipmentId && item.IsActive)
            .Select(item => item.RentalPricePerDay)
            .FirstOrDefaultAsync();
    }

    public async Task<Equipment?> GetEquipmentByCondition(Expression<Func<Equipment, bool>> expression)
    {
        return await _context.EquipmentItems.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByCondition(Expression<Func<Equipment, bool>> expression)
    {
        return await _context.EquipmentItems
            .Include(item => item.Category)
            .Where(expression)
            .ToListAsync();
    }

    public Task<IEnumerable<Equipment>> GetEquipmentsByCondition(Expression<Func<bool, Equipment>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId)
    {
        return await _context.EquipmentItems.Where(item => item.IsActive && item.Id == equipmentId)
            .Select(item => item.Status)
            .FirstOrDefaultAsync();
    }
    public async  Task<bool> IsEquipmentUnique(Equipment equipment, Guid? excludeId = null)
    {
        return !await _context.EquipmentItems
            .AnyAsync(item => (item.Name == equipment.Name || item.SerialNumber == equipment.SerialNumber
            && (excludeId == null || item.Id != excludeId)));
    }

}
