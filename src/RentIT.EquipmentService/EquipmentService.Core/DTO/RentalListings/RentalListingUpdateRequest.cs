using EquipmentService.Core.Attributes;
using EquipmentService.Core.DTO.Images;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.DTO.RentalListings;

public class RentalListingUpdateRequest
{
    [Required]
    [StringLength(150, MinimumLength = 5)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required]
    public CurrencyRequest PricePerDay { get; set; } = null!;

    [Required]
    public CurrencyRequest PenaltyFeePerDay { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Maximum rental days must be greater than 0")]
    public int MaximumRentalDays { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum rental days must be greater than 0")]
    public int MinimumRentalDays { get; set; }

    [Required]
    [MaxCount(10, ErrorMessage = "You can upload up to 10 images per equipment.")]
    public IEnumerable<ImageRequest> Images { get; set; } = [];
}
