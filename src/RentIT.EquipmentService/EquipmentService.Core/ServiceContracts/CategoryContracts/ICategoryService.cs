using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.ServiceContracts.CategoryContracts;

public interface ICategoryService
{
    Task<Result<CategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken = default);
    Task<Result<CreatedResponse>> AddCategory(CategoryAddRequest request);
    Task<Result> DeleteCategory(Guid id);
    Task<Result<UpdatedResponse>> UpdateCategory(Guid id, CategoryUpdateRequest request);
    Task<Result<IReadOnlyCollection<SelectItem>>> GetAllCategories(CancellationToken cancellationToken = default);
}
