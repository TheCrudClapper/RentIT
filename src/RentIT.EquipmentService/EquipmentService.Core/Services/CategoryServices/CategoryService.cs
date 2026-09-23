using EquipmentService.Core.Domain.Entities.Categories;
using EquipmentService.Core.Domain.Entities.Categories.Errors;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.DTO.Shared;
using EquipmentService.Core.Mappings;
using EquipmentService.Core.ServiceContracts;

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

        entity.Deactivate();
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

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.DateEdited = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        return entity.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<SelectItem>>> GetAllCategories(CancellationToken ct)
    {
        IReadOnlyCollection<Category> categories = await _categoryRepository.GetAllAsync(ct);

        return categories
            .Select(item => new SelectItem { Id = item.Id, Name = item.Name })
            .ToList();
    }

    public async Task<Result<CategoryResponse>> GetCategory(Guid id, CancellationToken ct)
    {
        Category? entity = await _categoryRepository.GetByIdAsync(id, ct: ct, asNoTracking: true);

        return entity is null
            ? Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound)
            : entity.ToCategoryResponse();
    }
}
