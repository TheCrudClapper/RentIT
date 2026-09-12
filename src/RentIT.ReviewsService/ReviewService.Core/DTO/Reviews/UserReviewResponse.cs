namespace ReviewService.Core.DTO.Reviews;

public record UserReviewResponse(Guid Id, string Description, decimal Rating);