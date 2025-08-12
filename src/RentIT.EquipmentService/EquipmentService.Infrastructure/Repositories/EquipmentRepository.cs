using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace RentIT.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly EquipmentContext _context;
        public EquipmentRepository(EquipmentContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEquipmentAsync(Guid equipmentId)
        {
            var equipment = await GetActiveEquipmentByIdAsync(equipmentId);

            if (equipment == null)
                return false;

            equipment.DateDeleted = DateTime.UtcNow;
            equipment.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DoesEquipmentExistsAsync(Guid equipmentId)
        {
            return await _context.EquipmentItems
                .AnyAsync(item => item.Id == equipmentId && item.IsActive);
        }

        public async Task<Equipment?> GetActiveEquipmentByIdAsync(Guid equipmentId)
        {
            return await _context.EquipmentItems
                .Include(item => item.Category)
                .FirstOrDefaultAsync(item => item.IsActive && item.Id == equipmentId);
        }

        public async Task<IEnumerable<Equipment>> GetAllActiveEquipmentItemsAsync()
        {
            return await _context.EquipmentItems.Where(item => item.IsActive)
                .Include(item => item.Category)
                .ToListAsync();
        }
        public async Task<decimal?> GetDailyPriceAsync(Guid equipmentId)
        {
            return await _context.EquipmentItems
                .Where(item => item.Id == equipmentId && item.IsActive)
                .Select(item => item.RentalPricePerDay)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DoesEquipmentBelongsToUser(Guid equipmentId, Guid userId)
        {
            return await _context.EquipmentItems
                .AnyAsync(item => item.IsActive && item.Id == equipmentId && item.CreatedByUserId == userId);
        }

        public async Task<RentStatusEnum?> GetEquipmentStatusAsync(Guid equipmentId)
        {
            return await _context.EquipmentItems.Where(item => item.IsActive && item.Id == equipmentId)
                .Select(item => item.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<Equipment> AddEquipmentAsync(Equipment equipment)
        {
            equipment.Id = Guid.NewGuid();
            _context.EquipmentItems.Add(equipment);
            await _context.SaveChangesAsync();
            await _context.Entry(equipment).Reference(item => item.Category).LoadAsync();
            return equipment;
        }

        public async Task<bool> UpdateEquipmentAsync(Guid equipmentId, Equipment equipment)
        {
            var equipmentToUpdate = await GetActiveEquipmentByIdAsync(equipmentId);

            if (equipmentToUpdate == null)
                return false;

            equipmentToUpdate.Name = equipment.Name;
            equipmentToUpdate.Status = equipment.Status;
            equipmentToUpdate.Notes = equipment.Notes;
            equipmentToUpdate.RentalPricePerDay = equipment.RentalPricePerDay;
            equipmentToUpdate.SerialNumber = equipment.SerialNumber;
            equipmentToUpdate.CategoryId = equipment.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEquipmentUnique(Equipment equipment, Guid? excludeId = null)
        {
            return !await _context.EquipmentItems
                .AnyAsync(item => item.IsActive
                    && (item.Name == equipment.Name || item.SerialNumber == equipment.SerialNumber)
                    && (excludeId == null || item.Id != excludeId));
        }
    }
}
