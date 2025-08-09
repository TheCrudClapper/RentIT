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

            if(!await _categoryRepository.IsCategoryUnique(category))
                return Result.Failure<CategoryResponse>(CategoryErrors.CategoryAlreadyExists);

            Category newCategory = await _categoryRepository.AddCategoryAsync(category);

            return Result.Success(newCategory.ToCategoryResponse());
        }

        public async Task<Result> DeleteCategory(Guid categoryId)
        {
            bool isSuccess = await _categoryRepository.DeleteCategoryAsync(categoryId);

            if (!isSuccess)
                return Result.Failure(CategoryErrors.CategoryNotFound);

            return Result.Success();
        }

        public async Task<Result> UpdateCategory(Guid categoryId, CategoryUpdateRequest request)
        {
            Category categoryToEdit = request.ToCategory();

            if(!await _categoryRepository.IsCategoryUnique(categoryToEdit, categoryId))
                return Result.Failure(CategoryErrors.CategoryAlreadyExists);

            bool isSuccess = await _categoryRepository.UpdateCategoryAsync(categoryId, categoryToEdit);

            if (!isSuccess)
                return Result.Failure(CategoryErrors.CategoryNotFound);

            return Result.Success();
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(item => item.ToCategoryResponse());
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
