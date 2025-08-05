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
    }
}
