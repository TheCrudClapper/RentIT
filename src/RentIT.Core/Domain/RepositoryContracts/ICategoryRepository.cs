using RentIT.Core.Domain.Entities;

namespace RentIT.Core.Domain.RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
        Task<Category?> GetCategoryByIdAsync(Guid categoryId);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
}
