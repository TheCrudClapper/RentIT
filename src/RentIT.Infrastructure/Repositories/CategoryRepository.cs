using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Infrastructure.DbContexts;
using System.Threading.Tasks;

namespace RentIT.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            category.IsActive = true;
            category.DateCreated = DateTime.UtcNow;
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            Category? category = await _dbContext.Categories
                .FirstOrDefaultAsync(item => item.Id == categoryId && item.IsActive);

            if (category == null)
                return false;

            category.DateDeleted = DateTime.UtcNow;
            category.IsActive = false;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Where(item => item.IsActive)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(item => item.Id == categoryId && item.IsActive);
        }

        public async Task<bool> UpdateCategoryAsync(Guid categoryId, Category category)
        {
            Category? categoryToEdit = await GetCategoryByIdAsync(categoryId);

            if (categoryToEdit == null)
                return false;

            categoryToEdit.Name = category.Name;
            categoryToEdit.Description = category.Description;
            categoryToEdit.DateEdited = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsEntityValid(Category dbObject)
        {
            return await _dbContext.Categories.AnyAsync(item => item.Name != dbObject.Name);
        }
    }
}
