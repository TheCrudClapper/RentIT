using Microsoft.EntityFrameworkCore;
using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Infrastructure.DbContexts;

namespace EquipmentService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EquipmentContext _context;
        public CategoryRepository(EquipmentContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            Category? category = await GetCategoryByIdAsync(categoryId);

            if (category == null)
                return false;

            category.DateDeleted = DateTime.UtcNow;
            category.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(item => item.Id == categoryId);
        }

        public async Task<bool> UpdateCategoryAsync(Guid categoryId, Category category)
        {
            Category? categoryToEdit = await GetCategoryByIdAsync(categoryId);

            if (categoryToEdit == null)
                return false;

            categoryToEdit.Name = category.Name;
            categoryToEdit.Description = category.Description;
            categoryToEdit.DateEdited = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsCategoryUnique(Category entity, Guid? excludeId = null)
        {
            return !await _context.Categories
                .AnyAsync(item => item.Name == entity.Name
                && (excludeId == null || item.Id != excludeId));
        }

        public async Task<bool> DoesCategoryExist(Guid categoryId)
        {
            return await _context.Categories
                .AnyAsync(item => item.Id == categoryId);
        }
    }
}
