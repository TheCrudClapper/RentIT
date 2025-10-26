using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public class EquipmentRepository : BaseEquipmentRepository, IEquipmentRepository
{
    public EquipmentRepository(EquipmentContext context) : base(context){ }

    public async Task DeleteEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
    {
        equipment.DateDeleted = DateTime.UtcNow;
        equipment.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId, CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .Include(item => item.Category)
            .FirstOrDefaultAsync(item => item.Id == equipmentId, cancellationToken);
    }

    public async Task<Equipment> AddEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
    {
        equipment.Id = Guid.NewGuid();
        _context.EquipmentItems.Add(equipment);
        await _context.SaveChangesAsync(cancellationToken);

        await _context.Entry(equipment).Reference(item => item.Category).LoadAsync(cancellationToken);
        return equipment;
    }
 
    public async Task<Equipment?> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment, CancellationToken cancellationToken)
    {
        var equipmentToUpdate = await GetEquipmentByIdAsync(equipmentId, cancellationToken);

        if (equipmentToUpdate == null)
            return null;

        equipmentToUpdate.Name = equipment.Name;
        equipmentToUpdate.Status = equipment.Status;
        equipmentToUpdate.Notes = equipment.Notes;
        equipmentToUpdate.CreatedByUserId = equipment.CreatedByUserId;  
        equipmentToUpdate.RentalPricePerDay = equipment.RentalPricePerDay;
        equipmentToUpdate.SerialNumber = equipment.SerialNumber;
        equipmentToUpdate.CategoryId = equipment.CategoryId;

        await _context.Entry(equipmentToUpdate).Reference(item => item.Category).LoadAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return equipmentToUpdate;
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync(CancellationToken cancellationToken)
    {
        return await _context.EquipmentItems
            .AsNoTracking()
            .Where(item => item.IsActive)
            .Include(item => item.Category)
            .ToListAsync(cancellationToken);
    }
    public async Task UpdateEquipmentRating(Equipment equipment, decimal newAverageRating, int reviewCountToAdd = 0)
    {
        equipment.AverageRating = newAverageRating;
        equipment.ReviewCount = equipment.ReviewCount + reviewCountToAdd;
        await _context.SaveChangesAsync();
    }
}
