using EquipmentService.Core.Domain.Entities.Categories;
using EquipmentService.Core.Domain.Entities.Categories.Errors;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ServiceContracts.CategoryContracts;

namespace EquipmentService.Core.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreatedResponse>> AddCategory(CategoryAddRequest request)
    {
        Category entity = request.ToCategory();

        if (!await _categoryRepository.IsCategoryUnique(entity))
            return Result.Failure<CreatedResponse>(CategoryErrors.CategoryAlreadyExists);

        await _categoryRepository.AddAsync(entity);

        await _unitOfWork.SaveChangesAsync();
        return entity.ToCreatedResponse();
    }

    public async Task<Result> DeleteCategory(Guid categoryId)
    {
        Category? entity = await _categoryRepository.GetByIdAsync(categoryId);
        if (entity is null)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        entity.IsActive = false;
        entity.DateDeleted = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<UpdatedResponse>> UpdateCategory(Guid categoryId, CategoryUpdateRequest request)
    {
        Category? entity = await _categoryRepository.GetByIdAsync(categoryId);
        if (entity is null)
            return Result.Failure<UpdatedResponse>(CategoryErrors.CategoryNotFound);

        if (!await _categoryRepository.IsCategoryUnique(entity, categoryId))
            return Result.Failure<UpdatedResponse>(CategoryErrors.CategoryAlreadyExists);

        entity.Name = entity.Name;
        entity.Description = entity.Description;
        entity.DateEdited = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        return entity.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<SelectItem>>> GetAllCategories(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Category> categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(item => new SelectItem { Id = item.Id, Name = item.Name })
            .ToList();
    }

    public async Task<Result<CategoryResponse>> GetCategory(Guid id, CancellationToken ct)
    {
        Category? entity = await _categoryRepository.GetByIdAsync(id, ct: ct);

        return entity is null
            ? Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound)
            : entity.ToCategoryResponse();
    }
}
