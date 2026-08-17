using System.ComponentModel.DataAnnotations;

namespace ReviewService.Core.DTO.Reviews;

public record ReviewUpdateRequest
{
    [MaxLength(1024)]
    [Required]
    public string Description { get; set; } = null!;

    [Range(1, 5)]
    public decimal Rating { get; set; }

}