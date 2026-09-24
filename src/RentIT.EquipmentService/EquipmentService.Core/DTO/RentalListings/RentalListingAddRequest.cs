using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.DTO.RentalListings;

public class RentalListingAddRequest
{
    [Required]
    public Guid EquipmentId { get; set; }

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
}

public class CurrencyRequest
{
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string CurrencyCode { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Amount { get; set; }
}
