using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Core.DTO.ReviewAllowance;
using ReviewService.Core.ServiceContracts;

namespace ReviewService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReviewAllowanceController : ControllerBase
{
    private readonly IReviewAllowanceService _reviewAllowanceService;
    public ReviewAllowanceController(IReviewAllowanceService reviewAllowanceService)
    {
        _reviewAllowanceService = reviewAllowanceService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewAllowanceService.GetReviewAllowance(id, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    //Testing only
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ReviewAllowanceResponse>>> GetAllReviewAllowances (CancellationToken cancellation)
    {
        var result = await _reviewAllowanceService.GetAllReviewAllowances(cancellation);

        if(result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return Ok(result.Value);
    }
}
