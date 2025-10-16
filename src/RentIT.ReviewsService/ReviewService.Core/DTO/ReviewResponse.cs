namespace ReviewServices.Core.DTO;

public record ReviewResponse(
    string UserEmail,
    string Description,
    double Rating);
