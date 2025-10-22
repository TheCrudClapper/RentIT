namespace ReviewService.Core.DTO.Review;

public record UserReviewResponse(Guid Id, string Description, double Rating);