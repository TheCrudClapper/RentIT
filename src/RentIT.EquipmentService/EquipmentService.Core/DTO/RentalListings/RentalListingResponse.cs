using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.DTO.RentalListings;

public class RentalListingResponse
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public CurrencyResponse PricePerDay { get; set; } = null!;
    public CurrencyResponse PenaltyFeePerDay { get; set; } = null!;
    public int MaximumRentalDays { get; set; }
    public int MinimumRentalDays { get; set; }
    public int ListingStatus { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateEdited { get; set; }
}

