using System.ComponentModel.DataAnnotations;

namespace ReviewService.Core.DTO.Review;

public record ReviewUpdateRequest 
{
    [MaxLength(1024)]
    [Required]
    public string Description { get; set; } = null!;

    [Range(1, 5)]
    public decimal Rating { get; set; }

} 