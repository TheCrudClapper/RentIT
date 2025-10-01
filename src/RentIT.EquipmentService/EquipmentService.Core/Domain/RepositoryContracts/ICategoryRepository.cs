using EquipmentService.Core.Domain.Entities;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Category> AddCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<bool> UpdateCategoryAsync(Guid categoryId, Category category, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<bool> IsCategoryUnique(Category dbObject, CancellationToken cancellationToken, Guid? excludeId = null);
    Task<bool> DoesCategoryExist(Guid categoryId, CancellationToken cancellationToken);
}

