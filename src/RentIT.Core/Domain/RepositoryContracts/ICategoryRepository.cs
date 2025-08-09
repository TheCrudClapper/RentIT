using RentIT.Core.Domain.Entities;

namespace RentIT.Core.Domain.RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Guid categoryId, Category category);
        Task<Category?> GetCategoryByIdAsync(Guid categoryId);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<bool> IsCategoryUnique(Category dbObject, Guid? excludeId = null);
        Task<bool> DoesCategoryExist(Guid categoryId);
    }
}
