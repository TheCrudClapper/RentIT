using Microsoft.AspNetCore.Mvc;
using ReviewServices.API.Extensions;
using ReviewServices.Core.DTO;
using ReviewServices.Core.ServiceContracts;

namespace ReviewServices.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IUserReviewService _reviewService;
    private Guid ICurrentUser => this.GetLoggedUserId();
    public ReviewsController(IUserReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: api/<ReviewsController>
    [HttpGet("/byEquipment{reviewId}")]
    public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAllReviewsForEquipment(Guid reviewId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // GET api/<ReviewsController>/5
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponse>> GetReview(int reviewId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // POST api/<ReviewsController>
    [HttpPost]
    public async Task<ActionResult<ReviewResponse>> PostReview(ReviewAddRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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
