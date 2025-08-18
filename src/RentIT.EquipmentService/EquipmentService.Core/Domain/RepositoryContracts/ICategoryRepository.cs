using EquipmentService.Core.Domain.Entities;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
    Task<Category> AddCategoryAsync(Category category);
    Task<bool> UpdateCategoryAsync(Guid categoryId, Category category);
    Task<Category?> GetActiveCategoryByIdAsync(Guid categoryId);
    Task<bool> DeleteCategoryAsync(Guid categoryId);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<bool> IsCategoryUnique(Category dbObject, Guid? excludeId = null);
    Task<bool> DoesCategoryExist(Guid categoryId);
}

