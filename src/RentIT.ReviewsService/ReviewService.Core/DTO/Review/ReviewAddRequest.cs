using System.ComponentModel.DataAnnotations;


namespace ReviewService.Core.DTO.Review;

public record ReviewAddRequest(
    [Required] [MaxLength(1024)] string Description,
    [Required] [Range(1, 5)] double Rating,
    [Required] Guid RentalId
    );
