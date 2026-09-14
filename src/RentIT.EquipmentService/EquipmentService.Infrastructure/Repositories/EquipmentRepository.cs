using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public class EquipmentRepository : BaseEquipmentRepository, IEquipmentRepository
{
    public EquipmentRepository(EquipmentContext context) : base(context) { }

    public async Task UpdateEquipmentRating(Equipment equipment, decimal newAverageRating, int reviewCountToAdd = 0)
    {
        equipment.AverageRating = newAverageRating;
        equipment.ReviewCount = Math.Max(0, equipment.ReviewCount + reviewCountToAdd);
        await _context.SaveChangesAsync();
    }
}
