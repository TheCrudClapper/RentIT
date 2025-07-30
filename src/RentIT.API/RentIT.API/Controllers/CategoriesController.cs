using Microsoft.AspNetCore.Mvc;
using RentIT.Core.DTO.CategoryDto;
using RentIT.Core.ServiceContracts;

namespace RentIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> PostCategory(CategoryAddRequest request)
        {
            var result = await _categoryService.AddCategory(request);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: 400);

            return result.Value;
        }

        //GET :api/Categories/categoryId
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(Guid categoryId)
        {
            var result = await _categoryService.GetCategory(categoryId);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return result.Value;
        }

        //DELETE: api/Categories/categoryId
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);

            if (result.IsFailure)
                return Problem(detail: result.Error.Description, statusCode: result.Error.ErrorCode);

            return NoContent();
        }
    }
}
