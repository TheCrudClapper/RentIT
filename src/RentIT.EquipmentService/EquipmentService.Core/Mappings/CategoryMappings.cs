using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.DTO.CategoryDto;

namespace EquipmentService.Core.Mappings;

public static class CategoryMappings
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
    public static Category ToCategory(this CategoryAddRequest request)
    {
        return new Category
        {
            Description = request.Description,
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            Name = request.Name
        };
    }
    public static Category ToCategory(this CategoryUpdateRequest request)
    {
        return new Category
        {
            Name = request.Name,
            Description = request.Description,
        };
    }
}

