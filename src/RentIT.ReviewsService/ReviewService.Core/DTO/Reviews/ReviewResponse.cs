namespace ReviewService.Core.DTO.Reviews;

public record ReviewResponse(
    Guid Id,
    string UserEmail,
    string Description,
    decimal Rating);
