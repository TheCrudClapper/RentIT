using EquipmentService.Core.Domain.Entities.Categories;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(EquipmentContext context) : base(context) { }

    public async Task<bool> IsCategoryUnique(Category entity, Guid? excludeId = null)
    {
        return !await _context.Categories
            .AnyAsync(item => item.Name == entity.Name
            && (excludeId == null || item.Id != excludeId));
    }

    public async Task<bool> ExistsAsync(Guid categoryId)
    {
        return await _context.Categories
            .AnyAsync(item => item.Id == categoryId);
    }
}
