using RentIT.Core.DTO.CategoryDto;
using RentIT.Core.ResultTypes;

namespace RentIT.Core.ServiceContracts
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
