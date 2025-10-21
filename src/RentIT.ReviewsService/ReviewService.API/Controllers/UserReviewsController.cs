using Microsoft.AspNetCore.Mvc;
using ReviewService.Core.DTO.Review;
using ReviewServices.API.Extensions;
using ReviewServices.Core.ServiceContracts;

namespace ReviewService.API.Controllers;

public class UserReviewsController : ControllerBase
{
    private readonly IUserReviewService _userReviewService;
    private Guid CurrentUser => this.GetLoggedUserId();
    public UserReviewsController(IUserReviewService reviewService)
    {
        _userReviewService = reviewService;
    }


    // GET api/<ReviewsController>/5
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var response = await _userReviewService.GetReview(CurrentUser, reviewId, cancellationToken);

        if (response.IsFailure)
            return Problem(detail: response.Error.Description, statusCode: response.Error.StatusCode);

        return response.Value;
    }

    // POST api/<ReviewsController>
    [HttpPost]
    public async Task<ActionResult<ReviewResponse>> PostReview(ReviewAddRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    // PUT api/<ReviewsController>/5
    [HttpPut("{reviewId}")]

    public Task<IActionResult> PutReview(Guid reviewId, ReviewUpdateRequest request, CancellationToken cancellationToken)
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
