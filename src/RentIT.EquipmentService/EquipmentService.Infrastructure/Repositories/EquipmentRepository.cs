using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories
{
    public class EquipmentRepository : BaseEquipmentRepository, IEquipmentRepository
    {
        public EquipmentRepository(EquipmentContext context) : base(context){ }

        public async Task DeleteEquipmentAsync(Equipment equipment)
        {
            equipment.DateDeleted = DateTime.UtcNow;
            equipment.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(Guid equipmentId)
        {
            return await _context.EquipmentItems
                .Include(item => item.Category)
                .FirstOrDefaultAsync(item => item.Id == equipmentId);
        }

        public async Task<Equipment> AddEquipmentAsync(Equipment equipment)
        {
            equipment.Id = Guid.NewGuid();
            _context.EquipmentItems.Add(equipment);
            await _context.SaveChangesAsync();
            await _context.Entry(equipment).Reference(item => item.Category).LoadAsync();
            return equipment;
        }
     
        public async Task<Equipment?> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment)
        {
            var equipmentToUpdate = await GetEquipmentByIdAsync(equipmentId);

            if (equipmentToUpdate == null)
                return null;

            equipmentToUpdate.Name = equipment.Name;
            equipmentToUpdate.Status = equipment.Status;
            equipmentToUpdate.Notes = equipment.Notes;
            equipmentToUpdate.CreatedByUserId = equipment.CreatedByUserId;  
            equipmentToUpdate.RentalPricePerDay = equipment.RentalPricePerDay;
            equipmentToUpdate.SerialNumber = equipment.SerialNumber;
            equipmentToUpdate.CategoryId = equipment.CategoryId;

            await _context.Entry(equipmentToUpdate).Reference(item => item.Category).LoadAsync();
            await _context.SaveChangesAsync();
            
            return equipmentToUpdate;
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
        {
            return await _context.EquipmentItems
                .AsNoTracking()
                .Where(item => item.IsActive)
                .Include(item => item.Category)
                .ToListAsync();
        }
    }
}
