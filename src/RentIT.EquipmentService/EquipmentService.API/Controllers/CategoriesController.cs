using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.ServiceContracts.CategoryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryResponse>> PostCategory(CategoryAddRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.AddCategory(request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    //GET :api/Categories/categoryId
    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetCategory(categoryId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    //GET :api/Categories
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllCategories(cancellationToken);
        return Ok(categories);
    }

    //PUT :api/Categories
    [HttpPut("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateCategory(categoryId, request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    //DELETE: api/Categories/categoryId
    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteCategory(categoryId, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }
}

