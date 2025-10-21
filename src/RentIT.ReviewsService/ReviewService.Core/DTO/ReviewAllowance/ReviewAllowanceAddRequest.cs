using System.ComponentModel.DataAnnotations;

namespace ReviewService.Core.DTO.ReviewAllowance;
/// <summary>
/// Represents a dto for allowance add request.
/// </summary>
public record ReviewAllowanceAddRequest
{
    [Required]
    public Guid UserId { get; init; }

    [Required]
    public Guid EquipmentId { get; init; }

    [Required]
    public Guid RentalId { get; init; }
}
