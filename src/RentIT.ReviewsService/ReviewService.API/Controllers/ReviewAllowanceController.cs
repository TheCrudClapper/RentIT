using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Core.DTO.ReviewAllowance;
using ReviewService.Core.ServiceContracts;

namespace ReviewService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReviewAllowanceController : BaseApiController
{
    private readonly IReviewAllowanceService _reviewAllowanceService;
    public ReviewAllowanceController(IReviewAllowanceService reviewAllowanceService)
    {
        _reviewAllowanceService = reviewAllowanceService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewAllowanceResponse>> GetReviewAllowance(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _reviewAllowanceService.GetReviewAllowance(id, cancellationToken));

    //Testing only
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<ReviewAllowanceResponse>>> GetAllReviewAllowances(CancellationToken cancellation)
        => HandleResult(await _reviewAllowanceService.GetAllReviewAllowances(cancellation));
}
