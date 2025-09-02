using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public abstract class BaseEquipmentRepository : IBaseEquipmentRepository
{
    protected readonly EquipmentContext _context;
    protected BaseEquipmentRepository(EquipmentContext context)
    {
        _context = context;
    }
    public async virtual Task<bool> DoesEquipmentExistsAsync(Guid equipmentId)
    {
        return await _context.EquipmentItems
            .AnyAsync(item => item.Id == equipmentId);
    }

    public async virtual Task<decimal?> GetDailyPriceAsync(Guid equipmentId)
    {
        return await _context.EquipmentItems
            .Where(item => item.Id == equipmentId && item.IsActive)
            .Select(item => item.RentalPricePerDay)
            .FirstOrDefaultAsync();
    }

    public async virtual Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId)
    {
        return await _context.EquipmentItems.Where(item => item.IsActive && item.Id == equipmentId)
            .Select(item => item.Status)
            .FirstOrDefaultAsync();
    }
    public async virtual Task<bool> IsEquipmentUnique(Equipment equipment, Guid? excludeId = null)
    {
        return !await _context.EquipmentItems
            .AnyAsync(item => (item.Name == equipment.Name || item.SerialNumber == equipment.SerialNumber
            && (excludeId == null || item.Id != excludeId)));
    }
}
