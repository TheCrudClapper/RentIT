using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public class UserEquipmentRepository : BaseEquipmentRepository, IUserEquipmentRepository
{
    public UserEquipmentRepository(EquipmentContext context) : base(context) { }
    public async Task<Equipment> AddUserEquipment(Equipment equipment, Guid userId, CancellationToken cancellationToken)
    {
        equipment.Id = Guid.NewGuid();
        equipment.CreatedByUserId = userId;
        _context.EquipmentItems.Add(equipment);
        await _context.SaveChangesAsync(cancellationToken);
        await _context.Entry(equipment).Reference(item => item.Category).LoadAsync(cancellationToken);
        return equipment;
    }

    public async Task<bool> UpdateUserEquipmentAsync(Guid equipmentId, Equipment equipment, CancellationToken cancellationToken)
    {
        Equipment? equipmentToUpdate = await GetUserEquipmentByIdAsync(equipment.CreatedByUserId ,equipmentId, cancellationToken);

        if (equipmentToUpdate == null)
            return false;

        equipmentToUpdate.Name = equipment.Name;
        equipmentToUpdate.Status = equipment.Status;
        equipmentToUpdate.Notes = equipment.Notes;
        equipmentToUpdate.RentalPricePerDay = equipment.RentalPricePerDay;
        equipmentToUpdate.SerialNumber = equipment.SerialNumber;
        equipmentToUpdate.CategoryId = equipment.CategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .AsNoTracking()
            .Include(item => item.Category)
            .Where(item => item.CreatedByUserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Equipment?> GetUserEquipmentByIdAsync(Guid equipmentId, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
               .FirstOrDefaultAsync(item => item.Id == equipmentId && item.CreatedByUserId == userId, cancellationToken);
    }

    public async Task DeleteUserEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
    {
        equipment.DateDeleted = DateTime.UtcNow;
        equipment.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

}
