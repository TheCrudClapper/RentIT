using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.ServiceContracts.CategoryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/categories")]
[Authorize]
[ApiController]
public class CategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
        => _categoryService = categoryService;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryResponse>> PostCategory(CategoryAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.AddCategory(request, cancellationToken));

    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.GetCategory(categoryId, cancellationToken));

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyCollection<CategoryResponse>>> GetAllCategories(CancellationToken cancellationToken)
        => HandleResult(await _categoryService.GetAllCategories(cancellationToken));

    [HttpPut("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.UpdateCategory(categoryId, request, cancellationToken));

    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.DeleteCategory(categoryId, cancellationToken));
}
