namespace ReviewService.Core.DTO.Review;

public record ReviewResponse(
    Guid Id,
    string UserEmail,
    string Description,
    decimal Rating);
