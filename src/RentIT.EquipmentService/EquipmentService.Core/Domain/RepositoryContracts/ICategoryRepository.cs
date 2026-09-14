using EquipmentService.Core.Domain.Entities.Categories;

namespace EquipmentService.Core.Domain.RepositoryContracts;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> IsCategoryUnique(Category dbObject, Guid? excludeId = null);
    Task<bool> DoesCategoryExist(Guid categoryId);
}

