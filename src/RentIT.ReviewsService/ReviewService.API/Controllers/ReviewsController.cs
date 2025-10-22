using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.API.Controllers;
using ReviewService.Core.DTO.Review;
using ReviewService.Core.ServiceContracts;

namespace ReviewServices.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReviewsController : BaseApiController
{
    private readonly IReviewService _reviewService;
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: api/Reviews
    [HttpGet("byEquipment/{equipmentId}")]
    public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAllReviewsForEquipment(Guid equipmentId, CancellationToken cancellationToken)
        => HandleResult(await _reviewService.GetReviewsByEquipmentId(equipmentId, cancellationToken));

    // GET api/Reviews/5
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken)
        => HandleResult(await _reviewService.GetReview(reviewId, cancellationToken));

    // DELETE api/Reviews/5
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId, CancellationToken cancellationToken)
        => HandleResult(await _reviewService.DeleteReview(reviewId, cancellationToken));
}
