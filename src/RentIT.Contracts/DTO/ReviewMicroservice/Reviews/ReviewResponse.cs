namespace RentIT.Contracts.DTO.ReviewMicroservice.Reviews;

public record ReviewResponse(
    Guid Id,
    string UserEmail,
    string Description,
    decimal Rating);

