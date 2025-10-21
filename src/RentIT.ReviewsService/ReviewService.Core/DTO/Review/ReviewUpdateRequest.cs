using System.ComponentModel.DataAnnotations;

namespace ReviewService.Core.DTO.Review;

public record ReviewUpdateRequest 
{
    [MaxLength(1024)]
    [Required]
    public string Description { get; set; } = null!;

    [Range(1, 5)]
    public double Rating { get; set; }

    [Required]
    public Guid RentalId { get; set;  }
} 