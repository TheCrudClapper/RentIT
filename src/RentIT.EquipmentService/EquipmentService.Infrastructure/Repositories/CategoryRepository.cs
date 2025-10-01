using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EquipmentService.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly EquipmentContext _context;
    public CategoryRepository(EquipmentContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        category.Id = Guid.NewGuid();
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        Category? category = await GetCategoryByIdAsync(categoryId, cancellationToken);

        if (category == null)
            return false;

        category.DateDeleted = DateTime.UtcNow;
        category.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(item => item.Id == categoryId, cancellationToken);
    }

    public async Task<bool> UpdateCategoryAsync(Guid categoryId, Category category, CancellationToken cancellationToken)
    {
        Category? categoryToEdit = await GetCategoryByIdAsync(categoryId, cancellationToken);

        if (categoryToEdit == null)
            return false;

        categoryToEdit.Name = category.Name;
        categoryToEdit.Description = category.Description;
        categoryToEdit.DateEdited = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> IsCategoryUnique(Category entity, CancellationToken cancellationToken, Guid? excludeId = null)
    {
        return !await _context.Categories
            .AnyAsync(item => item.Name == entity.Name
            && (excludeId == null || item.Id != excludeId), cancellationToken);
    }

    public async Task<bool> DoesCategoryExist(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AnyAsync(item => item.Id == categoryId, cancellationToken);
    }
}
