using RentIT.Core.Domain.Entities;
using RentIT.Core.DTO.CategoryDto;

namespace RentIT.Core.Mappings
{
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
}
