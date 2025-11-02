using EquipmentService.Core.DTO.CategoryDto;
using EquipmentService.Core.ServiceContracts.CategoryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentService.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
        => _categoryService = categoryService;

    // POST: api/Categories
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryResponse>> PostCategory(CategoryAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.AddCategory(request, cancellationToken));

    // GET: api/Categories/{categoryId}
    [HttpGet("{categoryId}")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.GetCategory(categoryId, cancellationToken));

    // GET: api/Categories
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories(CancellationToken cancellationToken)
        => HandleResult(await _categoryService.GetAllCategories(cancellationToken));

    // PUT: api/Categories/{categoryId}
    [HttpPut("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.UpdateCategory(categoryId, request, cancellationToken));

    // DELETE: api/Categories/{categoryId}
    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
        => HandleResult(await _categoryService.DeleteCategory(categoryId, cancellationToken));
}
