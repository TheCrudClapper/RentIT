using System.ComponentModel.DataAnnotations;


namespace RentIT.Contracts.DTO.ReviewMicroservice.Reviews;

public record ReviewAddRequest(
    [Required][MaxLength(1024)] string Description,
    [Required][Range(1, 5)] decimal Rating,
    [Required] Guid RentalId
    );

