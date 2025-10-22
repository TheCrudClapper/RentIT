using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Core.DTO.Review;
using ReviewServices.Core.ServiceContracts;

namespace ReviewService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserReviewsController : BaseApiController
{
    private readonly IUserReviewService _userReviewService;
    public UserReviewsController(IUserReviewService reviewService)
    {
        _userReviewService = reviewService;
    }

    // GET api/UserReviews/5
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<ReviewResponse>> GetReview(Guid reviewId, CancellationToken cancellationToken)
        => HandleResult(await _userReviewService.GetUserReview(CurrentUserId, reviewId, cancellationToken));

    // POST api/UserReviews
    [HttpPost]
    public async Task<ActionResult<ReviewResponse>> PostReview(ReviewAddRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    // PUT api/UserReviews/5
    [HttpPut("{reviewId}")]
    public async Task<ActionResult<UserReviewResponse>> PutReview(Guid reviewId, ReviewUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _userReviewService.UpdateUserReview(CurrentUserId, reviewId, request, cancellationToken));

    // DELETE api/UserReviews/5
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId, CancellationToken cancellationToken)
        => HandleResult(await _userReviewService.DeleteUserReview(CurrentUserId, reviewId, cancellationToken));
}
