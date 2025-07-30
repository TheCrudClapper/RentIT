using Microsoft.EntityFrameworkCore;
using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Infrastructure.DbContexts;

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

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(item => item.Id == categoryId && item.IsActive);
        }
    }
}
