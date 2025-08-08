using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Infrastructure.DbContexts;

namespace RentIT.Infrastructure.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EquipmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEquipmentAsync(Guid equipmentId)
        {
            var equipment = await  GetActiveEquipmentByIdAsync(equipmentId);

            if(equipment == null)
                return false;

            equipment.DateDeleted = DateTime.UtcNow;
            equipment.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> DoesEquipmentExistsAsync(Guid equipmentId)
        {
            return _context.EquipmentItems
                .AnyAsync(item => item.Id == equipmentId && item.IsActive);
        }

        public async Task<Equipment?> GetActiveEquipmentByIdAsync(Guid equipmentId)
        {
            return await _context.EquipmentItems
                .Include(item => item.CreatedBy)
                .Include(item => item.Category)
                .Include(item => item.Rentals)
                .FirstOrDefaultAsync(item => item.IsActive);
        }

        public async Task<IEnumerable<Equipment>> GetAllActiveEquipmentItemsAsync()
        {
            return await _context.EquipmentItems.Where(item => item.IsActive)
                .Include(item => item.CreatedBy)
                .Include(item => item.Category)
                .Include(item => item.Rentals)
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
    }
}
