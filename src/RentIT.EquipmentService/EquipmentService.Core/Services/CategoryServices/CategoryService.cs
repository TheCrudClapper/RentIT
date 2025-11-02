using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ResultTypes;
using EquipmentService.Core.ServiceContracts.CategoryContracts;

namespace EquipmentService.Core.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryResponse>> AddCategory(CategoryAddRequest request, CancellationToken cancellationToken)
    {
        Category category = request.ToCategory();

        if (!await _categoryRepository.IsCategoryUnique(category, cancellationToken))
            return Result.Failure<CategoryResponse>(CategoryErrors.CategoryAlreadyExists);

        Category newCategory = await _categoryRepository.AddCategoryAsync(category, cancellationToken);

        return Result.Success(newCategory.ToCategoryResponse());
    }

    public async Task<Result> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        bool isSuccess = await _categoryRepository.DeleteCategoryAsync(categoryId, cancellationToken);

        if (!isSuccess)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        return Result.Success();
    }

    public async Task<Result> UpdateCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        Category categoryToEdit = request.ToCategory();

        if (!await _categoryRepository.IsCategoryUnique(categoryToEdit, cancellationToken, categoryId))
            return Result.Failure(CategoryErrors.CategoryAlreadyExists);

        bool isSuccess = await _categoryRepository.UpdateCategoryAsync(categoryId, categoryToEdit, cancellationToken);

        if (!isSuccess)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<CategoryResponse>>> GetAllCategories(CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);

        return categories
            .Select(item => item.ToCategoryResponse())
            .ToList();
    }

    public async Task<Result<CategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);

        if (category == null)
            return Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound);

        return category.ToCategoryResponse();
    }
}
