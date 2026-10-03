using EquipmentService.Core.DTO.Images;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.DTO.RentalListings;

public class UserRentalListingListItem
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public CurrencyResponse PricePerDay { get; set; } = null!;
    public int ListingStatus { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateEdited { get; set; }
    public ImageResponse Thumbnail { get; set; } = null!;
}
