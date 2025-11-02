using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.ResultTypes;

namespace EquipmentService.Core.ServiceContracts.CategoryContracts;

public interface ICategoryService
{
    Task<Result<CategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken = default);
    Task<Result<CategoryResponse>> AddCategory(CategoryAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteCategory(Guid id, CancellationToken cancellationToken = default);
    Task<Result> UpdateCategory(Guid id, CategoryUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<CategoryResponse>>> GetAllCategories(CancellationToken cancellationToken = default);
}
