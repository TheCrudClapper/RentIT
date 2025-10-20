using Microsoft.AspNetCore.Mvc;
using ReviewService.Core.ServiceContracts;
using ReviewServices.Core.DTO;

namespace ReviewServices.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: api/<ReviewsController>
    [HttpGet("byEquipment/{equipmentId}")]
    public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAllReviewsForEquipment(Guid equipmentId, CancellationToken cancellationToken)
    {
        var response = await _reviewService.GetReviewsByEquipmentId(equipmentId, cancellationToken);

        if (response.IsFailure)
            return Problem(detail: response.Error.Description, statusCode: response.Error.StatusCode);

        return Ok(response.Value);
    }

    // GET api/<ReviewsController>/5
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var response = await _reviewService.GetReview(reviewId, cancellationToken);

        if (response.IsFailure)
            return Problem(detail: response.Error.Description, statusCode: response.Error.StatusCode);

        return response.Value;
    }

    // PUT api/<ReviewsController>/5
    [HttpPut("{reviewId}")]

    public Task<IActionResult> PutReview (Guid reviewId, ReviewUpdateRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // DELETE api/<ReviewsController>/5
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
