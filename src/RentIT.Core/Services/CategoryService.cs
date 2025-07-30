using RentIT.Core.Domain.Entities;
using RentIT.Core.Domain.RepositoryContracts;
using RentIT.Core.DTO.CategoryDto;
using RentIT.Core.Mappings;
using RentIT.Core.ResultTypes;
using RentIT.Core.ServiceContracts;

namespace RentIT.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<CategoryResponse>> AddCategory(CategoryAddRequest request)
        {
            Category category = request.ToCategory();

            Category newCategory = await _categoryRepository.AddCategoryAsync(category);

            return Result.Success(newCategory.ToCategoryResponse());
        }

        public async Task<Result> DeleteCategory(Guid id)
        {
            bool isSuccess = await _categoryRepository.DeleteCategoryAsync(id);

            if (!isSuccess)
                return Result.Failure(CategoryErrors.CategoryNotFound);

            return Result.Success();
        }

        public async Task<Result<CategoryResponse>> GetCategory(Guid id)
        {
            Category? category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound);

            return category.ToCategoryResponse();
        }
    }
}
