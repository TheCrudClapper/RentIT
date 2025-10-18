namespace ReviewServices.Core.DTO;

public record ReviewResponse(
    Guid Id,
    string UserEmail,
    string Description,
    double Rating);
