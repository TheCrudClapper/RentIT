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

    [HttpPost]
    public async Task<ActionResult<ReviewAllowanceResponse>> PostReviewAllowance(ReviewAllowanceAddRequest request, CancellationToken cancellationToken)
    {
        var result = await _reviewAllowanceService.AddReviewAllowance(request, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return result.Value;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reviewAllowanceService.GetReviewAllowance(id, cancellationToken);

        if (result.IsFailure)
            return Problem(detail: result.Error.Description, statusCode: result.Error.StatusCode);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReviewAllowanceResponse>> DeleteReviewAllowance(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
