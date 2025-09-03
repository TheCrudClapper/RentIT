using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories
{
    public class UserEquipmentRepository : BaseEquipmentRepository, IUserEquipmentRepository
    {
        public UserEquipmentRepository(EquipmentContext context) : base(context) { }
        public async Task<Equipment> AddUserEquipment(Equipment equipment, Guid userId)
        {
            equipment.Id = Guid.NewGuid();
            equipment.CreatedByUserId = userId;
            _context.EquipmentItems.Add(equipment);
            await _context.SaveChangesAsync();
            await _context.Entry(equipment).Reference(item => item.Category).LoadAsync();
            return equipment;
        }

        public async Task<bool> UpdateUserEquipmentAsync(Guid equipmentId, Equipment equipment)
        {
            Equipment? equipmentToUpdate = await GetUserEquipmentByIdAsync(equipment.CreatedByUserId ,equipmentId);

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

        public async Task<IEnumerable<Equipment>> GetAllUserEquipmentAsync(Guid userId)
        {
            return await _context.EquipmentItems
                .Where(item => item.CreatedByUserId == userId)
                .ToListAsync();
        }

        public async Task<Equipment?> GetUserEquipmentByIdAsync(Guid equipmentId, Guid userId)
        {
            return await _context.EquipmentItems
                   .FirstOrDefaultAsync(item => item.Id == equipmentId && item.CreatedByUserId == userId);
        }

        public async Task<bool> DeleteUserEquipmentAsync(Guid userId, Guid equipmentId)
        {
            var equipment = await GetUserEquipmentByIdAsync(userId, equipmentId);
            if (equipment == null)
                return false;

            equipment.DateDeleted = DateTime.UtcNow;
            equipment.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
