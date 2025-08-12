using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts
{
    public interface ICategoryService
    {
        Task<Result<CategoryResponse>> GetCategory(Guid id);
        Task<Result<CategoryResponse>> AddCategory(CategoryAddRequest request);
        Task<Result> DeleteCategory(Guid id);
        Task<Result> UpdateCategory(Guid id, CategoryUpdateRequest request);
        Task<IEnumerable<CategoryResponse>> GetAllCategories();
    }
}
